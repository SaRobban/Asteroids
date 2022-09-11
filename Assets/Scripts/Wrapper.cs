using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    [SerializeField] private Vector2 screenHalfBounds;

    [SerializeField] private GameObject clonePreFab;
    private Transform cloneX;
    private Transform cloneY;
    Vector3 cPos => Camera.main.transform.position;
    Vector3 myPos => transform.position;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfBounds = GetScreenBounds();
        cloneX = Instantiate(clonePreFab, this.transform).transform;
        cloneY = Instantiate(clonePreFab, this.transform).transform;
        PlaceClones();
    }
    Vector2 GetScreenBounds()
    {
        Debug.Log("Screen Height : " + Screen.height);
        Debug.Log("Screen Width : " + Screen.width);
        float aspct = (float)Screen.width / (float)Screen.height;
        float hight = Camera.main.orthographicSize;
        float with = hight * aspct;

        return new Vector2(with, hight);
    }

    // Update is called once per frame
    void Update()
    {
        ScreenWarp();
        PlaceClones();
    }
    void ScreenWarp()
    {

        Debug.DrawLine(cPos + (Vector3)screenHalfBounds, cPos - (Vector3)screenHalfBounds, Color.red);


        if (myPos.x > cPos.x + screenHalfBounds.x)
        {
            transform.position -= Vector3.right * screenHalfBounds.x * 2;
        }
        else if (myPos.x < cPos.x - screenHalfBounds.x)
        {
            transform.position += Vector3.right * screenHalfBounds.x * 2;
        }

        if (myPos.y > cPos.y + screenHalfBounds.y)
        {
            transform.position -= Vector3.up * screenHalfBounds.y * 2;
        }
        else if (myPos.y < cPos.y - screenHalfBounds.y)
        {
            transform.position += Vector3.up * screenHalfBounds.y * 2;
        }
    }

    void PlaceClones()
    {
        if (cPos.x < myPos.x)
        {
            cloneX.position = myPos - Vector3.right * screenHalfBounds.x * 2;
        }
        else
        {
            cloneX.position = myPos + Vector3.right * screenHalfBounds.x * 2;
        }

        if (cPos.y < myPos.y)
        {
            cloneY.position = myPos - Vector3.up * screenHalfBounds.y * 2;
        }
        else
        {
            cloneY.position = myPos + Vector3.up * screenHalfBounds.y * 2;
        }
    }
}
