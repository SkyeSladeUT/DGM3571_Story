using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visible : MonoBehaviour
{
    public In_Camera_View CamRange;
    public Targeting TargetScript;

    private void FixedUpdate()
    {
        CheckInRange();
    }

    public bool CheckInRange()
    {
        if (CamRange.InRange(transform))
        {
            if(!TargetScript.EnemiesInRange.Contains(gameObject))
                TargetScript.EnemiesInRange.Add(gameObject);
            return true;
        }
        TargetScript.EnemiesInRange.Remove(gameObject);
        return false;
    }
}
