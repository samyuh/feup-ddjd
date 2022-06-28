using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAgathe : MonoBehaviour {
    [SerializeField] private FollowPlayer _agatheFollow;

    void Awake() {
        _agatheFollow.enabled = false;
    }

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            _agatheFollow.enabled = true;
        }
    }
}
