using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Open(){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
    }

    void Close(){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
    }
}
