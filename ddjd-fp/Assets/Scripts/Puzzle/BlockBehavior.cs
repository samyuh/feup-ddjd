using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
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

    void move(Vector3 playerPosition) {
        if (gameObject.transform.position.x - playerPosition.x > 0.5) {
            Debug.Log("Move X");
            gameObject.transform.position += new Vector3(1, 0, 0);
        }
        if (gameObject.transform.position.x - playerPosition.x < -0.5) {
            Debug.Log("Move X");
            gameObject.transform.position += new Vector3(-1, 0, 0);
        }

        if (gameObject.transform.position.z - playerPosition.z > 0.5) {
            Debug.Log("Move Z");
            gameObject.transform.position += new Vector3(0, 0, 1);
        }
        if (gameObject.transform.position.z - playerPosition.z < -0.5) {
            Debug.Log("Move Z");
            gameObject.transform.position += new Vector3(0, 0, -1);
        }
    }
}
