using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonFunctions
{
    public static Transform FindClosestTransformInList(Transform t, List<Transform> list, float maxDist = Mathf.Infinity)
    {
        Transform closest = null;
        foreach (Transform snap in list)
        {
            float dist = (t.position - snap.position).sqrMagnitude;
            if (dist < maxDist)
            {
                closest = snap;
                maxDist = dist;
            }
        }
        return closest;
    }

    public static bool OverLapingInList(Collider2D collider, List<Collider2D> listOfColliders)
    {
        foreach (Collider2D col in listOfColliders)
        {
            if (Physics2D.IsTouching(collider, col))
            {
                return true;
            }
        }
        return false;
    }


    //Scene View functions
    public static bool InView(Camera cam, Vector3 pos, Vector2 offsett)
    {
        Vector2 screenPos = cam.WorldToScreenPoint(pos);
       
        if (screenPos.x < offsett.x)
            return false;

        if (screenPos.y < offsett.y)
            return false;

        if (screenPos.x > Screen.width - offsett.x)
            return false;

        if (screenPos.y > Screen.height - offsett.y)
            return false;

        return true;
    }

    public static Vector3 ClosestPositionInView(Camera cam, Vector3 pos, Vector2 offsett)
    {
        Vector3 screenPos = cam.WorldToScreenPoint(pos);

        if (screenPos.x < offsett.x)
            screenPos.x = offsett.x;

        if (screenPos.y < offsett.y)
            screenPos.y = offsett.y;

        float xMax = Screen.width - offsett.x;
        if (screenPos.x > xMax)
            screenPos.x = xMax;

        float yMax = Screen.height - offsett.y;
        if (screenPos.y > yMax)
            screenPos.y = yMax;

        return cam.ScreenToWorldPoint(screenPos);
    }
}