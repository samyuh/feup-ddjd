using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalWheelController: MonoBehaviour {
    private bool weaponWheelSelected = false;
    private static int _idCrystal = 1;

    private void Awake() {
        gameObject.SetActive(weaponWheelSelected);

        Events.OnToggleCrystalWheel.AddListener(OnToggleCrystalWheel);
        Events.OnChangeSelectedCrystal.AddListener(OnChangeSelectedCrystal);
    }

    private void Update() {
        if(weaponWheelSelected) {
            // Get cursor displacement using Look

            // Calculations to draw a line with cursor displacement. The start will be the center

            // Make hover effect
            
            switch (_idCrystal) {
                case 1:
                    Debug.Log("Crystal 1");
                    break;
                case 2:
                    Debug.Log("Crystal 2");
                    break;
                 case 3:
                    Debug.Log("Crystal 3");
                    break;
                case 4:
                    Debug.Log("Crystal 4");
                    break;
            }
        }
    }

    private void OnChangeSelectedCrystal(int idCrystal) {
        _idCrystal = idCrystal;
    }

    private void OnToggleCrystalWheel() {
        weaponWheelSelected = !weaponWheelSelected;

        // Don't do this in the final version
        if (weaponWheelSelected) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }

        gameObject.SetActive(weaponWheelSelected);
    }
}