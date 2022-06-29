using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    private bool playerIsOverlapping = false;
    [SerializeField] private bool canTeleport;

    [SerializeField] string currentScene;
    [SerializeField] string nextScene;

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

    public void Activate(){
        canTeleport = true;
    }

    public void Deactivate(){
        canTeleport = false;
    }
}
