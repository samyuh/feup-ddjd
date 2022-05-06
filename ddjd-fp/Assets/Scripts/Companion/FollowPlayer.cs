using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {   

    public float stopDistance = 0.25f;
    public float followDistance = 3f;
    private float speed = 0f;
    public float maxSpeed = 0.025f;
    public float acceleration = 0.00001f;
    public float deceleration = 0.1f;


    
    
    private GameObject _target;

    private void Start(){
        _target = GameObject.Find("Player");
    }

    private void Update() {
        transform.LookAt(_target.transform.position);
        
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit)) {
            float distance = hit.distance;

            // Only follow after a certain _distance from the _target
            // Follows the _target until its close to his head

            if(distance > followDistance) accelerate();
            else decelarate();

            Move();
        }
    }

    private void accelerate(){
        speed += acceleration * Time.deltaTime;
        if(speed > maxSpeed) speed = maxSpeed;
    }
    private void decelarate(){
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;
    }

    private void Move() {
        // Value is hard coded for 1f vertical to be placed aproxximately in the _target's head (should change de code or the value if you wish to scale the _target size)
        transform.position = Vector3.MoveTowards(transform.position,_target.transform.position + new Vector3(0f, 1f, 0f) ,speed);
    }
}
