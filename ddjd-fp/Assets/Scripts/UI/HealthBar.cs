using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar: MonoBehaviour {
    private Slider _healthBar;
    
    private void Awake() {
       _healthBar = GetComponent<Slider>();

        Events.OnHealthUpdate.AddListener(HealthUpdate);
    }

    private void HealthUpdate(int currentHealth, int maxHealth) {
        _healthBar.value = (float) currentHealth / (float) maxHealth;
    }
}
