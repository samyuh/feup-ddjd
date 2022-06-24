using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatePlatform : MonoBehaviour {
    public int speed;
    public float distance;
    public GameObject armature;
    public GameObject collider;

    private bool move = false;
    private Vector3 _startPositionArmature;
    private Vector3 _startPositionCollider;

    private Vector3 _targetPositionArmature;
    private Vector3 _targetPositionCollider;
    private float _elapsedTime;

    void Start() {
        _startPositionArmature = armature.transform.position;
        _startPositionCollider = collider.transform.position;
        _elapsedTime = 0f;

        _targetPositionArmature = new Vector3( _startPositionArmature.x,  _startPositionArmature.y + distance,  _startPositionArmature.z);
        _targetPositionCollider = new Vector3( _startPositionCollider.x,  _startPositionCollider.y + distance,  _startPositionCollider.z);
    }

    void Update() {
        if (move) {
            _elapsedTime += Time.deltaTime;

            float distanceDisplacement = _elapsedTime * speed;
            float interpolationRatio = distanceDisplacement / distance;
            armature.transform.position = Vector3.Lerp(_startPositionArmature, _targetPositionArmature, interpolationRatio);
            collider.transform.position = Vector3.Lerp(_startPositionCollider, _targetPositionCollider, interpolationRatio);
        }  
        else {
            _elapsedTime += Time.deltaTime;

            float distanceDisplacement = _elapsedTime * speed;
            float interpolationRatio = distanceDisplacement / distance;
            armature.transform.position = Vector3.Lerp(_targetPositionArmature, _startPositionArmature,  interpolationRatio);
            collider.transform.position = Vector3.Lerp(_targetPositionCollider, _startPositionCollider,  interpolationRatio);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.tag == "Player") {
            move = true;
            _elapsedTime = 0f;
        }
    }

    private void OnTriggerExit(Collider collider) {
        if(collider.tag == "Player") {
            move = false;
            _elapsedTime = 0f;
        }
    }
}
