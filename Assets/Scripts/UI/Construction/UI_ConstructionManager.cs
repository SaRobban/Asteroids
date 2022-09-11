using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Convert to class. 
public class UI_ConstructionManager : MonoBehaviour
{
    [SerializeField] private RectTransform BoundBoxHolder;
    [SerializeField] private UI_ConstructionBoundKeyBox bindBoxPreFab;
    [SerializeField] private List< UI_ConstructionBoundKeyBox> bindBoxes;

    private void Start()
    {
        GameMaster.instance.shipMaster.A_OnShipPartAdded += AddBoundKeyUI;
        ResetDisplays();
    }
    private void OnDestroy()
    {
        GameMaster.instance.shipMaster.A_OnShipPartAdded -= AddBoundKeyUI;
    }

    public void ResetDisplays()
    {
        if (bindBoxes == null)
            bindBoxes = new List<UI_ConstructionBoundKeyBox>();
        else
            foreach (UI_ConstructionBoundKeyBox bindBox in bindBoxes)
                Destroy(bindBox);

        foreach (ShipComponent shipComponent in GameMaster.instance.shipMaster.shipComponentsList)
            AddBoundKeyUI(shipComponent);
    }

    public void AddBoundKeyUI(ShipComponent shipComponent)
    {
        if (bindBoxes == null)
            bindBoxes = new List<UI_ConstructionBoundKeyBox>();

        if (!shipComponent.HasBoundKey)
            return;

        UI_ConstructionBoundKeyBox newBindBox =
            Instantiate(bindBoxPreFab, BoundBoxHolder);
        newBindBox.Setup(shipComponent, this);
        bindBoxes.Add(newBindBox);
    }

    public void RemoveBindBoxFromList(UI_ConstructionBoundKeyBox bindBox)
    {
        bindBoxes.Remove(bindBox);
    }
}
