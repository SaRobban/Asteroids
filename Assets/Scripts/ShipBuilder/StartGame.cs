using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
  //  private GameObject ship;
    // Start is called before the first frame update
    public void Go()
    {
        DontDestroyOnLoad(GameMaster.instance.shipMaster.root.gameObject);
        SceneManager.LoadScene("Start");
    }
}
