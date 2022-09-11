using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SaveLoad : MonoBehaviour
{
    public void Load()
    {
        GameMaster.instance.shipMaster.LoadShipFromFile();
    }
    public void Save() { 
        GameMaster.instance.shipMaster.SaveShipToFile();
    }
}
