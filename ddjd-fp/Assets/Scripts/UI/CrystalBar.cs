using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalBar: MonoBehaviour {
    private Slider _crystalBar;
    
    private void Awake() {
       _crystalBar = GetComponent<Slider>();
    }
}