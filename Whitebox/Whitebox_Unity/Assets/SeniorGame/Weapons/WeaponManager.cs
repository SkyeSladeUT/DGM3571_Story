﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponBase currentWeapon;

    private void Start()
    {
        currentWeapon.Initialize();
    }
    
    
}
