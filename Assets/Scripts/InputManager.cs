using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float turn => AxisX();
    public static float accelerate => AxisY();

    public static float AxisX()
    {
        return Input.GetAxis("Horizontal");
    }

    public static float AxisY()
    {
        return Input.GetAxis("Vertical");
    }
}
