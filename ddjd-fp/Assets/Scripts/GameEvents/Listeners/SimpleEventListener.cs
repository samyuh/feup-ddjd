using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleEventListener : MonoBehaviour {
    [SerializeField] private SimpleEvent _gameEvent;
    [SerializeField] private UnityEvent _unityEvent;

    private void Awake() => _gameEvent.Subscribe(this);
    private void OnDestroy() => _gameEvent.Unsubscribe(this);

    public void RaiseEvent() => _unityEvent.Invoke();
}