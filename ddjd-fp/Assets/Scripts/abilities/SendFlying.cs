using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlying : MonoBehaviour
{

    void Start(){
        Debug.Log("Helloooo");

    }

    void Update(){
        transform.position += transform.forward * Time.deltaTime * 15f;
    }

    void OnCollisionEnter(Collision collision)
    {   
        
        Debug.Log("Collided with " + collision.collider.name);
        
        if (collision.body as Rigidbody)
        {
            Debug.Log("The object is " + collision.gameObject);
            Debug.Log("body is " + collision.gameObject.GetComponent<Rigidbody>());

            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 500,ForceMode.Force);
        }

    }



}
