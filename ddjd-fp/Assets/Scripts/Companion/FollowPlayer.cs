using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {   

    public float followDistance = 1f;
    private float speed = 0f;
    public float maxSpeed = 0.1f;
    public float acceleration = 0.05f;
    public float deceleration = 0.1f;


    private GameObject _target;
    private GameObject _player;    


    private void Start(){
        _player = GameObject.Find("Player");
        _target = GameObject.Find("CompanionPlace");
    }

    private void Update() {
        if(Physics.Raycast(transform.position, _target.transform.position - transform.position, out RaycastHit hit)) {
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

        transform.LookAt(_target.transform.position + new Vector3(0,0.7f,0));
    }
    private void decelarate(){
        speed -= deceleration * Time.deltaTime;
        if (speed < 0) speed = 0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _target.transform.rotation, 100f * Time.deltaTime);
    }

    private void Move() {
        // Value is hard coded for 1f vertical to be placed aproxximately in the _target's head (should change de code or the value if you wish to scale the _target size)
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + new Vector3(0f, 0.7f, 0f) ,speed);
    }

}
