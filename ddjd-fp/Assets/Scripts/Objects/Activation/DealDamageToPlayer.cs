using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToPlayer : MonoBehaviour{
    public float time;
    
    private float _elapsedTime = 0f;
    private float _damageTime = 0f;

    private GameObject player;

    void Update() {
        _elapsedTime += Time.deltaTime;
        _damageTime -= Time.deltaTime;

        if (_damageTime < 0f) {
            if (player != null) {
                player.SendMessage("ApplyDamage", 50f);
            }
    
            _damageTime = 0.4f;
        }

        if (time < _elapsedTime) Destroy(gameObject);
    }

    void OnTriggerStay(Collider collision){   
        if (collision.gameObject.tag == "Player") {
            player = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider collision){   
        if (collision.gameObject.tag == "Player") {
            player = null;
        }
    }
}
