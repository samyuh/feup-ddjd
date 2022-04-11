using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventGeneric<T>: ScriptableObject {
    protected HashSet<T> _listeners = new HashSet<T>();

    public void Subscribe(T gameEventListener) => _listeners.Add(gameEventListener);
    public void Unsubscribe(T gameEventListener) => _listeners.Remove(gameEventListener);
}
