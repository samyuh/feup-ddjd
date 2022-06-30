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
    private float pushStep = 0.0f;

    void Update()
    {
        if(targetPosition != new Vector3(1000f, 1000f, 1000f) && transform.position != targetPosition){
            var step =  pushSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if(targetPosition == transform.position){
                //transform.position = targetPosition;
                Puzzle.SendMessage("Reset");
            }
                
            
        }

    }

 

    void MoveRequest(Vector3 playerPosition) {
        (int x, int z) direction = (0, 0);
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
        targetPosition = new Vector3(transform.position.x + direction.x * pushStep, transform.position.y, transform.position.z + direction.z * pushStep);
    }

    void SetPushStep(float step){
        pushStep = step;
    }
}
