using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [HideInInspector] public static GameMaster instance { private set; get; }
    public SO_ShipInterpreter shipInterpreter;
    public ShipMaster shipMaster;

    [SerializeField] public GameObject constructionMenuPreFab;
    public GameObject constructionMenu { private set; get; }

    [SerializeField] private Canvas worldCanvas;
    public Canvas WorldCanvas => worldCanvas;

    [SerializeField] private HUD_WorldManager controllDisplay;
    public HUD_WorldManager ControllHUD => controllDisplay;

    public enum GameState { DockMode, Space }
    GameState currentGameState;

    private void Awake()
    {
        MakeSingeltonOrDie();

        if (shipMaster == null)
        {
            shipMaster = new ShipMaster();
            Debug.Log("ShipCreated");
        }



        DontDestroyOnLoad(this.gameObject);

        //WARNING : TEST
        DontDestroyOnLoad(worldCanvas.gameObject);
    }

    public void SetUpOnLevelLoad(GameState levelState)
    {
        switch (levelState)
        {
            case GameState.DockMode:
                SetUpForDock();
                break;

            case GameState.Space:
                SetUpForSpace();
                break;
        }
    }

    private void SetUpForDock()
    {
        Debug.Log("Game in dockMode");
        if (constructionMenu == null)
        {
            constructionMenu = Instantiate(constructionMenuPreFab);
        }
        shipMaster.SetUpForDock();
    }
    private void SetUpForSpace()
    {
        Debug.Log("Game in spaceMode");
        if (constructionMenu != null)
        {
            Destroy(constructionMenu);
        }
        shipMaster.SetUpForFlight();
    }
    private void MakeSingeltonOrDie()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public GameObject InstantiateGameObject(GameObject gO)
    {
        return Instantiate(gO);
    }
    public ShipComponent InstantiateShipPrefab(ShipComponent shipPrefab)
    {
        return Instantiate(shipPrefab);
    }

    public void DestroyGameObject(GameObject gO)
    {
        Destroy(gO);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (constructionMenu != null)
                Destroy(constructionMenu);
            StartCoroutine(CreateOnWait());
        }
    }

    IEnumerator CreateOnWait()
    {
        yield return new WaitForSeconds(2);
        constructionMenu = Instantiate(constructionMenuPreFab);
    }
}
