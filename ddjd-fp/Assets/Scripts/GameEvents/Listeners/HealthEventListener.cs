using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthUnityEvent : UnityEvent<int, int> {}; 

public class HealthEventListener : MonoBehaviour {
    [SerializeField] private HealthEvent _gameEvent;
    [SerializeField] private HealthUnityEvent _unityEvent;

    private void Awake() {
        _gameEvent.Subscribe(this);

        if (_unityEvent == null)
            _unityEvent = new HealthUnityEvent();

        _unityEvent.AddListener(GetComponent<GameManager>().HealthUpdate);
    } 

    private void OnDestroy() => _gameEvent.Unsubscribe(this);

    public void RaiseEvent(int currentHealth, int maxHealth) {
       _unityEvent?.Invoke(currentHealth, maxHealth);
    }
}