using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShipConstructor : MonoBehaviour
{
    [Header("Scene")]
    Camera cam;
    [SerializeField] private Vector2 ScreenBoundsOffsettPx = new Vector2(100, 50);
    [SerializeField] private float maxSnapDist = 2;

    private List<Transform> snapPoints;
    private List<Collider2D> colliders;

    private ShipComponent selected;
    private bool canPlace;


    public void OnClickCreateSelection(GameObject newSelection)
    {
        OnClickDestroySelected();

        if (!newSelection.TryGetComponent(out ShipComponent shipPart))
        {
            Debug.LogError("selection is not a ShipComponent");
            return;
        }

        GameObject newSelected = Instantiate(newSelection);
        selected = newSelected.GetComponent<ShipComponent>();
    }
    public void OnClickDestroySelected()
    {
        if (selected != null)
        {
            ShipComponent toDump = selected;
            Destroy(toDump.gameObject);
            selected = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        Mouse.Instance.onMouseUpdate += OnMove;
        Mouse.Instance.onMouse1 += OnClickPlace;
        Mouse.Instance.onMouse2 += OnClickRemove;

        GameMaster.instance.shipMaster.A_OnShipLoaded += ResetConstructor;
        GameMaster.instance.shipMaster.A_OnShipPartAdded += AddToBuild;

        ResetConstructor();
    }
    private void OnDestroy()
    {
        Mouse.Instance.onMouseUpdate -= OnMove;
        Mouse.Instance.onMouse1 -= OnClickPlace;
        Mouse.Instance.onMouse2 -= OnClickRemove;

        GameMaster.instance.shipMaster.A_OnShipLoaded -= ResetConstructor;
        GameMaster.instance.shipMaster.A_OnShipPartAdded -= AddToBuild;
    }

    private void ResetConstructor()
    {
        snapPoints = new List<Transform>();
        colliders = new List<Collider2D>();

        foreach (ShipComponent shipComponent in GameMaster.instance.shipMaster.shipComponentsList)
        {
            AddToBuild(shipComponent);
        }
    }
    //Movement
    private void OnMove()
    {
        if (selected == null)
            return;

        canPlace = MoveCheckList();
        selected.SetOutLine(!canPlace);
    }
    private bool MoveCheckList()
    {
        Vector2 pos = Mouse.Instance.screenToWorldPos;
        if (OutOfBounds(pos))
            return false;

        if (CanSnapToBuild() && GameMaster.instance.shipMaster.shipComponentsList.Count > 0)
        {
            if (SelsectionOverLapsBuild())
                return false;
        }
        return true;
    }
    private bool OutOfBounds(Vector3 pos)
    {
        if (CommonFunctions.InView(cam, pos, ScreenBoundsOffsettPx))
        {
            selected.transform.position = pos;
            return false;
        }

        selected.transform.position = CommonFunctions.ClosestPositionInView(cam, pos, ScreenBoundsOffsettPx);
        return true;
    }
    private bool SelsectionOverLapsBuild()
    {
        foreach (Collider2D collider in selected.Colliders)
        {
            if (CommonFunctions.OverLapingInList(collider, colliders))
            {
                return true;
            }
        }
        return false;
    }
    private bool CanSnapToBuild()
    {
        Transform closestSnap =
            CommonFunctions.FindClosestTransformInList(selected.transform, snapPoints, maxSnapDist);

        if (closestSnap == null)
        {
            return false;
        }

        selected.transform.position = closestSnap.position;
        selected.transform.rotation = closestSnap.rotation;
        return true;
    }


    private void OnClickPlace()
    {
        if (selected == null)
            return;

        if (!canPlace)
            return;

        GameMaster.instance.shipMaster.AddShipComponent(selected);

    }

    private void AddToBuild(ShipComponent newPartToBuild)
    {
        snapPoints.AddRange(newPartToBuild.Snaps);
        colliders.AddRange(newPartToBuild.Colliders);
        newPartToBuild.A_OnDestroy += RemovePartFromBuild;
        selected = null;
    }


    private void OnClickRemove()
    {
        //If hit shipComponent
        foreach (ShipComponent shipComponent in GameMaster.instance.shipMaster.shipComponentsList)
        {
            foreach (Collider2D col in shipComponent.Colliders)
            {
                if (col.OverlapPoint(Mouse.Instance.screenToWorldPos))
                {
                    shipComponent.DestroyMe();
                    return;
                }
            }
        }
    }

    private void RemovePartFromBuild(ShipComponent shipComponent)
    {
        for (int i = 0; i < shipComponent.Colliders.Length; i++)
            colliders.Remove(shipComponent.Colliders[i]);

        for (int s = 0; s < shipComponent.Snaps.Length; s++)
            snapPoints.Remove(shipComponent.Snaps[s]);
    }
}
