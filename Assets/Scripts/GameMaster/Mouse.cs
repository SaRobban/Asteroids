using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Static class!?
public class Mouse : MonoBehaviour
{
    public static Mouse Instance { get; private set; }

    public Vector2 pos;
    public Vector2 screenToWorldPos;
    public Action onMouse1;
    public Action onMouse2;
    public Action onMouseUpdate;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseUpdate();

        if (Input.GetButtonDown("Fire1") && onMouse1 != null)
        {
            onMouse1();
        }

        if (Input.GetButtonDown("Fire2") && onMouse2 != null)
            onMouse2();
    }

    void MouseUpdate()
    {
        pos = Input.mousePosition;
        SetScreenToWorldPos();

        if (onMouseUpdate != null)
            onMouseUpdate();
    }

    void SetScreenToWorldPos()
    {
        screenToWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }



}
