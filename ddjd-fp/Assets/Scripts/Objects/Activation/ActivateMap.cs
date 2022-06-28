using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMap : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
