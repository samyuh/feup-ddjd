using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{   

    public float moveSpeed = 0.025f;
    public float stopDistance = 0.25f;
    public float followDistance = 3f;
    
    private bool move = false;
    private float distance;
    private GameObject player;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(player.transform.position);
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
            distance = hit.distance;

            Debug.Log(distance);

            // Only follow after a certain distance from the player
            // Follows the player until its close to his head
            if(!move && distance > followDistance) move = true;
            else if(distance < stopDistance) move = false;
            
            Move();
        }
    }

    private void Move(){
        // Value is hard coded for 1f vertical to be placed aproxximately in the player's head (should change de code or the value if you wish to scale the player size)
        if(move) transform.position = Vector3.MoveTowards(transform.position,player.transform.position + new Vector3(0f,1f,0f) ,moveSpeed);
    }


    
}
