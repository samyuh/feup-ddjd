using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAgathe : MonoBehaviour {
    private FollowPlayer _agatheFollow;

    void Awake() {
        _agatheFollow = GameObject.FindGameObjectWithTag("Companion").GetComponent<FollowPlayer>();
    }

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            _agatheFollow.enabled = true;
        }
    }
}
