using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;
    [SerializeField] bool canTeleport;
    [SerializeField] string currentScene;
    [SerializeField] string nextScene;
    /*[SerializeField] GameObject Info;

    private InfoScript infoScript;

    void Awake(){
        infoScript = Info.GetComponent<InfoScript>();
    }*/

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsOverlapping && canTeleport) {

            /*infoScript.savedPositions[currentScene] = player.transform.position;*/
            SceneManager.LoadScene(nextScene);
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
