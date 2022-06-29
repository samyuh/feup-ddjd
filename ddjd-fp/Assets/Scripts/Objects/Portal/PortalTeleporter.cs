using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    private bool playerIsOverlapping = false;
    [SerializeField] private bool canTeleport;

    [SerializeField] string currentScene = "1";
    [SerializeField] string nextScene = "2";

    void Update()
    {
        if(playerIsOverlapping && canTeleport) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(GameObject.Find("portal_" + nextScene + "_" + currentScene).transform.position);

            player.transform.position = GameObject.Find("portal_" + nextScene + "_" + currentScene).transform.position;
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

    void Activate(){
        canTeleport = true;
    }
}
