using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;

    void Start() {}

    // Update is called once per frame
    void Update()
    {
        if(playerIsOverlapping) {
            print("Teleporting player");
            //Vector3 portalToPlayer = player.position - transform.position;
            //float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //if(dotProduct < 0f) {
            //    float rotationDiff = - Quaternion.Angle(transform.rotation, receiver.rotation);
            //    rotationDiff += 180;
            //    player.Rotate(Vector3.up, rotationDiff);

            //    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            //    player.position = receiver.position + positionOffset;
            //
            //SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
                SceneManager.LoadScene("Isle 2");
                playerIsOverlapping = false;
        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") {
            playerIsOverlapping = false;
        }
    }
}
