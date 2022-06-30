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
            GameObject companion = GameObject.FindGameObjectWithTag("Companion");
            GameObject companionPlace = GameObject.Find("CompanionPlace");

            GameObject rootObject = GameObject.Find("island" + nextScene);
            GameObject grassNext = rootObject.transform.Find( "grass_" + nextScene).gameObject;
            GameObject grassCurrent = GameObject.Find("grass_" + currentScene);

            grassNext.SetActive(true);
            player.transform.position = GameObject.Find("portal_" + nextScene + "_" + currentScene).transform.position;
            companion.transform.position = companionPlace.transform.position;
            playerIsOverlapping = false;
            grassCurrent.SetActive(false);
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

    public void Activate() {
        canTeleport = true;
    }

    public void Deactivate(){
        canTeleport = false;
    }
}
