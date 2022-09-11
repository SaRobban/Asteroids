using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;

    [SerializeField] private GameMaster.GameState levelState = GameMaster.GameState.DockMode;
    private void Awake()
    {
        MakeSingeltonOrDie();
    }
    private void Start()
    {
        GameMaster.instance.SetUpOnLevelLoad(levelState);

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
    }
}
