using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/bool")]
public class BoolData : ScriptableObject
{
    public bool value;

    public void setBool(bool val)
    {
        value = val;
    }
}
