using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class In_Camera_View : MonoBehaviour
{
    //Place on Player Camera
    private Vector3 _camPos;
    public float MaxDistance;

    public bool InRange(Transform obj)
    {
        _camPos = GetComponent<Camera>().WorldToViewportPoint(obj.position);
        if ((_camPos.x > 0) && (_camPos.x < 1) && (_camPos.y > 0) && (_camPos.y < 1) && (_camPos.z > 0) && (_camPos.z < MaxDistance))
        {
            return true;
        }

        return false;
    }
    
}
