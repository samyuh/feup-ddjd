using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimController: MonoBehaviour 
{
    private bool _active = false;

    public void OnToggleAim() 
    {
        _active = !_active;
        gameObject.SetActive(_active);
    }
}