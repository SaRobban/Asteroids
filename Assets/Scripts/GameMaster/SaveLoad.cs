using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public static class SaveLoad
{
    private static string saveFileName = "GameSaveFile";

    [Serializable]
    public class SaveData
    {
        public ShipPartSaveData[] shipParts;
        public SaveData(ShipMaster shipMaster)
        {
            List<ShipPartSaveData> shipPartToData = new List<ShipPartSaveData>();

            foreach (ShipComponent part in shipMaster.shipComponentsList)
            {
                int i = part.name.IndexOf("(");
                string name = part.name.Substring(0, i);

                ShipPartSaveData addPartToArr =
                    new ShipPartSaveData(
                        name,
                        part.transform.position,
                        part.transform.rotation,
                        part.GetKeyBound()
                        );

                shipPartToData.Add(addPartToArr);
            }
            shipParts = shipPartToData.ToArray();
        }
    }
    [Serializable]
    public class ShipPartSaveData
    {
        public string name;
        public Vector2 position;
        public Quaternion rotation;
        public char key;

        public ShipPartSaveData(string name, Vector2 position, Quaternion rotation, char key)
        {
            this.name = name;
            this.position = position;
            this.rotation = rotation;
            this.key = key;
        }
    }

    //SAVE FILE////////////////////////////////////////////////////////////////////////////////////////
    public static void SaveToFile(ShipMaster shipMaster)
    {
        string storeData = GatherDataToJsonString(shipMaster);
        SaveToHDD(saveFileName, storeData);
    }

    private static string GatherDataToJsonString(ShipMaster shipMaster)
    {
        SaveData saveData = new SaveData(shipMaster);
        //Turn class into json string
        string jsonString = JsonUtility.ToJson(saveData);
        return jsonString;

    }
    /*
    //Prosses What should be saved to Json format
    private static string DataToJson(ShipMaster ship)
    {
        List<Vector2> partPositions = new List<Vector2>();
        List<Quaternion> partRotations = new List<Quaternion>();
        List<string> partNames = new List<string>();
        List<char> partKeys = new List<char>();

        foreach (ShipComponent comp in ship.shipComponentsList)
        {
            int i = comp.transform.name.IndexOf("(");
            string name = comp.transform.name.Substring(0, i);

            partPositions.Add(comp.transform.position);
            partRotations.Add(comp.transform.rotation);
            partNames.Add(name);
            partKeys.Add(comp.GetKeyBound());
        }
    

        SaveData storedData = new SaveData(
            partNames.ToArray(),
            partPositions.ToArray(),
            partRotations.ToArray(),
            partKeys.ToArray()
            );

        Debug.Log("#Names : " + partNames.ToArray().Length + "  Keys : " + partKeys.Count);
        //Turn class into json string
        string jsonString = JsonUtility.ToJson(storedData);
        return jsonString;
    }*/

    //Store on HDD
    public static void SaveToHDD(string fileName, string jsonString)
    {
        Debug.Log("Saved to : " + Application.persistentDataPath + fileName);
        File.WriteAllText(Application.persistentDataPath + fileName, jsonString);
        Debug.Log("Saved");
        return;
    }

    //LOAD FILE////////////////////////////////////////////////////////////////////////////////////////
    public static SaveData LoadFromFile()
    {
        string jsonString = LoadFromHDD(saveFileName);

        //Gate if empty
        if (string.IsNullOrEmpty(jsonString))
        {
            Debug.LogError("<color=red>Nothing to load</color>");
            return null;
        }

        SaveData storedData = JsonUtility.FromJson<SaveData>(jsonString);
        return storedData;
    }

    private static string LoadFromHDD(string fileName)
    {

        //Check if file exists / gate
        if (!File.Exists(Application.persistentDataPath + fileName))
            return null;

        return File.ReadAllText(Application.persistentDataPath + fileName);
    }
}
