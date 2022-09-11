using System;
using System.Collections.Generic;
using UnityEngine;
public class ShipComponent : MonoBehaviour
{
    [Header("physics")]
    [SerializeField] public float mass = 1;
    [Header("SubParts")]
    [SerializeField] private Transform[] snaps;
    [SerializeField] private Collider2D[] colliders;
    [SerializeField] private Renderer[] outline;
    public Transform[] Snaps => snaps;
    public Collider2D[] Colliders => colliders;

    [Header("Key")]
    [SerializeField] private bool hasBoundKey;
    public bool HasBoundKey => hasBoundKey;
    [SerializeField] private Vector3 inputUIOffset;
    public Vector3 InputUIOffset => inputUIOffset;
    [SerializeField] private char keyBound;
    KeyCode keyCode;

    [Header("RefrenceActions")]
    public Action A_OnKey;
    public Action<char> A_OnKeyBoundChanged;
    public Action A_OnUpdated;
    public Action<ShipComponent> A_OnDestroy;


    private void OnEnable()
    {
        SetOutLine(false);
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        A_OnDestroy?.Invoke(this);
    }

    private void Update()
    {
        A_OnUpdated?.Invoke();
        if (hasBoundKey)
        {
            if (Input.GetKey(keyCode))
            {
                A_OnKey();
            }
        }
    }

    public void SetKeyBound(char key)
    {
        if (!hasBoundKey)
            return;

        if (char.IsWhiteSpace(key))
        {
            Debug.Log("White");
            return;
        }

        keyBound = key;
        keyCode = GetKeyCode(key);
        Debug.Log(keyCode);
        A_OnKeyBoundChanged?.Invoke(keyBound);
    }

    // ShadyProductions -- https://answers.unity.com/questions/1611355/converting-string-of-a-key-into-keycode.html
    private KeyCode GetKeyCode(char character)
    {
        KeyCode code;
        code = (KeyCode)Enum.Parse(typeof(KeyCode), character.ToString());
        return code;
    }


    public char GetKeyBound()
    {
        return keyBound;
    }
    public void SetOutLine(bool to)
    {
        foreach (Renderer r in outline)
        {
            r.enabled = to;
        }
    }
    public void ExecuteOnKey(char c)
    {
        if (A_OnKey == null)
        {
            Debug.LogError("NoFunction Set " + transform.name);
            return;
        }

        if (c == keyBound)
            A_OnKey?.Invoke();
    }
}
