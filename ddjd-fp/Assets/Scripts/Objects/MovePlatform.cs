using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MovePlatform : MonoBehaviour {
    public List<GameObject> targetPositions;
    public int speed;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _distance;
    private float _elapsedTime;
    private int _nextPlatform;


    void Start() {

        _startPosition = targetPositions[0].transform.position;
        _targetPosition = targetPositions[1].transform.position;
        _nextPlatform = 1;

        _elapsedTime = 0f;
    }
 
    void Update() {
        if (transform.position == _targetPosition) {
            _startPosition = transform.position;
            _targetPosition =  targetPositions[_nextPlatform].transform.position;

            _distance = Vector3.Distance(_startPosition, _targetPosition);
            _elapsedTime = 0f;

            _nextPlatform = (_nextPlatform + 1) % targetPositions.Count;
        } 

        _elapsedTime += Time.fixedDeltaTime;

        // Distance moved equals elapsed time times speed
        float distanceDisplacement = _elapsedTime * speed / 10;
        float interpolationRatio = distanceDisplacement / _distance;

        transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolationRatio);
    }
}