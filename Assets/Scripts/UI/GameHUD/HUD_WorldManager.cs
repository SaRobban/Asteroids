using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HUD_WorldManager : MonoBehaviour
{
    [SerializeField] HUD_WorldControllDisplay displayPreFab;
    List<HUD_WorldControllDisplay> displays;

    private void Start()
    {
        GameMaster.instance.shipMaster.A_OnShipPartAdded += AddDisplay;
        ResetDisplays();
    }
    private void OnDestroy()
    {
        GameMaster.instance.shipMaster.A_OnShipPartAdded -= AddDisplay;
    }

    public void ResetDisplays()
    {
        if (displays == null)
            displays = new List<HUD_WorldControllDisplay>();
        else
            foreach (HUD_WorldControllDisplay display in displays)
                Destroy(display);
     
        foreach(ShipComponent shipComponent in GameMaster.instance.shipMaster.shipComponentsList)
            AddDisplay(shipComponent);
    }


    public void AddDisplay(ShipComponent shipComponent)
    {
        if (displays == null)
            displays = new List<HUD_WorldControllDisplay>();

        if (!shipComponent.HasBoundKey)
            return;

        HUD_WorldControllDisplay newDisplay = Instantiate(displayPreFab, transform);
        newDisplay.Setup(shipComponent);
        displays.Add(newDisplay);
        newDisplay.A_OnDestroy += RemoveDisplay;
    }

    public void RemoveDisplay(HUD_WorldControllDisplay display)
    {
        displays.Remove(display);
    }
}
