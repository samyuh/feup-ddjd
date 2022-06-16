using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] public GameObject Puzzle;
    Vector3 targetPosition = new Vector3(1000f, 1000f, 1000f);
    private float pushSpeed = 2.0f;
    private float constantSpeed = 0.001f;
    private float offset = 0.001f;
    // Start is called before the first frame update
    void Start()
    {

        /*if (gameObject.GetComponent<Rigidbody>().IsSleeping()) {
            gameObject.GetComponent<Rigidbody>().WakeUp();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != new Vector3(1000f, 1000f, 1000f) && transform.position != targetPosition){
            Vector3 differenceVector = targetPosition - transform.position;
            float differenceDistance = differenceVector.sqrMagnitude;

            //if(Mathf.Abs(differenceDistance) < offset){
            if(targetPosition == transform.position){
                //transform.position = targetPosition;
                Puzzle.SendMessage("Reset");
            }
            else{
                /*if(transform.position.x == targetPosition.x){
                    transform.position = new Vector3(transform.position.x + differenceVector.x * pushSpeed, transform.position.y, transform.position.z + differenceVector.z * pushSpeed + constantSpeed);
                }
                else{
                    transform.position = new Vector3(transform.position.x + differenceVector.x * pushSpeed + constantSpeed, transform.position.y, transform.position.z + differenceVector.z * pushSpeed);
                }*/
                var step =  pushSpeed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                
            }
        }
    }

    /*void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player") {
            GameObject playerObject = collisionInfo.gameObject;
            Player player = (Player) playerObject.GetComponent(typeof(Player));
            Debug.Log(player.StateMachine.CurrentState == );
            //if (player.StateMachine.CurrentState)
            
            Debug.Log("Essa vida!?");

            if (gameObject.transform.position.x - collisionInfo.gameObject.transform.position.x > 0.5) {
                Debug.Log("Move X");
                gameObject.transform.position += new Vector3(1, 0, 0);
            }
            if (gameObject.transform.position.x - collisionInfo.gameObject.transform.position.x < -0.5) {
                Debug.Log("Move X");
                gameObject.transform.position += new Vector3(-1, 0, 0);
            }

            if (gameObject.transform.position.z - collisionInfo.gameObject.transform.position.z > 0.5) {
                Debug.Log("Move Z");
                gameObject.transform.position += new Vector3(0, 0, 1);
            }
            if (gameObject.transform.position.z - collisionInfo.gameObject.transform.position.z < -0.5) {
                Debug.Log("Move Z");
                gameObject.transform.position += new Vector3(0, 0, -1);
            }
        }
    }*/

    void MoveRequest(Vector3 playerPosition) {
        (int x, int z) direction = (0, 0);
        /*Debug.Log("Player X: " + playerPosition.x);
        Debug.Log("Player Z: " + playerPosition.z);
        Debug.Log("Block X: " + gameObject.transform.position.x);
        Debug.Log("Block Z: " + gameObject.transform.position.z);*/
        if (gameObject.transform.position.x - playerPosition.x > 0.5) {
            direction = (1, 0);
        }
        else if (gameObject.transform.position.x - playerPosition.x < -0.5) {
            direction = (-1, 0);
        }

        else if (gameObject.transform.position.z - playerPosition.z > 0.5) {
            direction = (0, 1);
        }
        else if (gameObject.transform.position.z - playerPosition.z < -0.5) {
            direction = (0, -1);
        }

        (float x, float z) cubePosition = (gameObject.transform.position.x, gameObject.transform.position.z);

        Puzzle.gameObject.SendMessage("EvaluateMove", (cubePosition.x, cubePosition.z, direction.x, direction.z));

    }

    void Move((int x, int z) direction){
        targetPosition = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.z);
    }
}
