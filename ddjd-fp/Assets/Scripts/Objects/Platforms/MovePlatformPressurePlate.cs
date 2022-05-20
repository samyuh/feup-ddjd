using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformPressurePlate : MonoBehaviour {
    public int speed;
    public float distance;

    public bool move = false;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    
    private float _elapsedTime;

    void Start() {
        Events.OnPressurePlate.AddListener(PressurePlateUpdate);
        _startPosition = transform.position;
        _elapsedTime = 0f;

        _targetPosition = new Vector3( _startPosition.x,  _startPosition.y + distance,  _startPosition.z);
    }

    private void PressurePlateUpdate() {
        move = !move;
    }

    void Update() {
        if (move)
        {
            _elapsedTime += Time.deltaTime;

            float distanceDisplacement = _elapsedTime * speed;
            float interpolationRatio = distanceDisplacement / distance;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolationRatio);
        }       
    }
}
