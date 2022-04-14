using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Health Event")]
public class HealthEvent: GameEventGeneric<HealthEventListener> {
    public void Invoke(int currentHealth, int maxHealth) {
        foreach (var globalEventListener in _listeners) {
            globalEventListener.RaiseEvent(currentHealth, maxHealth);
        }
    }
}