using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Simple Game Event")]
public class SimpleEvent: GameEventGeneric<SimpleEventListener> {
    public void Invoke() {
        foreach (var globalEventListener in _listeners) {
            globalEventListener.RaiseEvent();
        }
    }
}