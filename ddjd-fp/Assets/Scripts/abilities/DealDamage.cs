using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour{
    void OnTriggerStay(Collider collision){   
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SendMessage("ApplyDamage", 100);
            Destroy(this);
        }
    }
}
