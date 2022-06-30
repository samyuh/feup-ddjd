using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalBar: MonoBehaviour {
    private Slider _crystalBar;
    
    private void Awake() {
       _crystalBar = GetComponent<Slider>();
       
       Events.OnCrystalManaUpdate.AddListener(CrystalManaUpdate);
    }

    private void CrystalManaUpdate(int currentCrystal, int maxCrystal) {
        _crystalBar.value = (float) currentCrystal / (float) maxCrystal;

        Debug.Log(currentCrystal );
        Debug.Log(maxCrystal);
        Debug.Log(_crystalBar.value);
    }
}