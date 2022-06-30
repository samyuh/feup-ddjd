using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour{

    public bool selfDestroy = false;
    void OnTriggerStay(Collider collision){   
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("ApplyDamage", 100);

            if (selfDestroy) {
                Destroy(gameObject);
            } else {
                Destroy(this);
            }
        }
    }
}
