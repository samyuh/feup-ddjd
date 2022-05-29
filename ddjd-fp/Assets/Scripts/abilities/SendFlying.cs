using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlying : MonoBehaviour
{
    void Update(){
        transform.position += transform.forward * Time.deltaTime * 15f;
    }

    void OnCollisionEnter(Collision collision){   
        
        // TODO: Colocar um If para saber quais objetos elevamos. (Deve dar por tag)
        if (collision.body as Rigidbody){
            GameObject obj = collision.gameObject;

            SendUpwards(obj);
            DealDamage(obj);
        }

    }

    private void SendUpwards(GameObject obj){
        obj.GetComponent<Rigidbody>().AddForce(transform.up * 500,ForceMode.Force);
    }


    private void DealDamage(GameObject obj){
        if(obj.tag == "Enemy"){
            obj.SendMessage("ApplyDamage",30);
        }
        
    }





}
