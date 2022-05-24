using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour{
    
    void Start(){
        Debug.Log("Hello, Im shooting Earth");
    }

    void OnCollisionEnter(Collision collision){   
        Debug.Log("Colliding");
        Debug.Log("Entered Collision with " + collision.gameObject.name);
        if (collision.body as Rigidbody && collision.gameObject.tag == "Enemy"){
            
            Debug.Log("Dealing 30 Damage");
            collision.gameObject.SendMessage("ApplyDamage",30);

        }
    }

}
