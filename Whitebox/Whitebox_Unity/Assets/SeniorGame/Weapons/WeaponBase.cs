using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public abstract void Initialize();
    public abstract IEnumerator Attack();
    public abstract void End();
    [HideInInspector]
    public bool currWeapon;
}
