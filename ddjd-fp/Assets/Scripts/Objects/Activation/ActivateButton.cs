using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour {
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _collider;

    public int portal;
    private bool _alreadyActive = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _distance;
    private float _elapsedTime = 0f;

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player" && !_alreadyActive) {
            Events.OnActivatePortal.Invoke(portal);
             _startPosition = _button.transform.position;
            _targetPosition =  new Vector3(_button.transform.position.x, _button.transform.position.y - 0.15f, _button.transform.position.z);
            _distance = Vector3.Distance(_startPosition, _targetPosition);
             _alreadyActive = true;
        }
    }

    void Update() {
        if (_alreadyActive) {
            if (_button.transform.position != _targetPosition) {
                // Distance moved equals elapsed time times speed
                _elapsedTime += Time.deltaTime;
                float distanceDisplacement = _elapsedTime * 0.15f;
                float interpolationRatio = distanceDisplacement / _distance;
                _button.transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolationRatio);
                _collider.transform.position = _button.transform.position;
            } 
        }
    }
}
