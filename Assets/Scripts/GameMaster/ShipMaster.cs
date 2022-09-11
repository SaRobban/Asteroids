using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipMaster
{
    public float mass = 0;
    public Transform root { private set; get; }
    public Rigidbody2D rootRB;
    public List<ShipComponent> shipComponentsList { private set; get; }

    public Action A_OnShipLoaded;
    public Action<ShipComponent> A_OnShipPartAdded;

    public ShipMaster()
    {
        CreateShipRoot();
    }

    private void CreateShipRoot()
    {
        root = new GameObject().transform;
        root.position = Vector3.zero;
        root.name = "NewShip";
        rootRB = root.gameObject.AddComponent<Rigidbody2D>();
        rootRB.constraints = RigidbodyConstraints2D.FreezeAll;
        root.gameObject.AddComponent<OnHitTEST>();


        shipComponentsList = new List<ShipComponent>();
    }
    public void AddShipComponent(ShipComponent shipComponent)
    {
        mass += shipComponent.mass;
        rootRB.mass += shipComponent.mass;

        shipComponent.transform.parent = root;
        shipComponentsList.Add(shipComponent);
        
        A_OnShipPartAdded?.Invoke(shipComponent);
        shipComponent.A_OnDestroy += RemoveShipComponentFromList;
    }
    private void RemoveShipComponentFromList(ShipComponent shipComponent)
    {
        mass -= shipComponent.mass;
        rootRB.mass -= shipComponent.mass;
        shipComponent.A_OnDestroy -= RemoveShipComponentFromList;
        shipComponentsList.Remove(shipComponent);
    }

    public void LoadShipFromFile()
    {
        SaveLoad.SaveData data = SaveLoad.LoadFromFile();
        if (data == null)
            return;

        DestroyAllChildren();
        InterpetLoadData(data);

        A_OnShipLoaded?.Invoke();
    }
    private void DestroyAllChildren()
    {
        ShipComponent[] destroyArr = new ShipComponent[shipComponentsList.Count];
        shipComponentsList.CopyTo(destroyArr);

        foreach (ShipComponent component in destroyArr)
            component.DestroyMe();
    }
    private void InterpetLoadData(SaveLoad.SaveData data)
    {
        shipComponentsList.Clear();
        GameObject[] shipPartsPrefabs = GameMaster.instance.shipInterpreter.shipparts;

        foreach (SaveLoad.ShipPartSaveData loadedPart in data.shipParts)
        {
            MatchLoadedDataWithPrefabList(loadedPart, shipPartsPrefabs);
        }
    }
    private void MatchLoadedDataWithPrefabList(SaveLoad.ShipPartSaveData loadedPart, GameObject[] prefabList)
    {
        foreach (GameObject preFab in prefabList)
        {
            if (loadedPart.name == preFab.name)
            {
                GameObject newPart = GameMaster.instance.InstantiateGameObject(preFab);
                {
                    if (newPart.TryGetComponent(out ShipComponent newShipPart))
                    {
                        newPart.transform.parent = root;
                        newPart.transform.position = loadedPart.position;
                        newPart.transform.rotation = loadedPart.rotation;
                        newShipPart.SetKeyBound(loadedPart.key);
                        AddShipComponent(newShipPart);
                    }
                    else
                    {
                        Debug.LogError("Loaded a shipPart that isnt a shipcomponent");
                    }
                }
                return;
            }
        }
        Debug.LogError("Loaded a shipPart that is not in shipcomponent List");
    }

    public void SaveShipToFile()
    {
        SaveLoad.SaveToFile(this);
    }

    public void SetUpForFlight()
    {
        rootRB.gravityScale = 0;
        rootRB.freezeRotation = false;
        rootRB.constraints = RigidbodyConstraints2D.None;
    }

    public void SetUpForDock()
    {
        rootRB.gravityScale = 0;
        rootRB.freezeRotation = true;
        rootRB.constraints = RigidbodyConstraints2D.FreezeAll;
        rootRB.MovePosition(Vector3.zero);
        rootRB.SetRotation(0);
    }

}
