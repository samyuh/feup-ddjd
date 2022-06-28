using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalWheelController: MonoBehaviour 
{
    private bool active = false;
    private static int _idCrystal = 1;

    private void Awake() {
        Events.OnSetActiveCrystal.AddListener(OnSetActiveCrystal);
    }

    private void Update() { }

    private void OnSetActiveCrystal(CrystalData crystal) 
    {
        _idCrystal = crystal.id;
    }

    public void OnToggleCrystalWheel() 
    {
        active = !active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        gameObject.SetActive(active);
    }
}