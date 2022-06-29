using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;
    private bool canTeleport;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] string nextScene;

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsOverlapping && canTeleport) {
            _gameManager.UpdateCurrentIsland(2);

            SceneManager.LoadScene(nextScene);
            playerIsOverlapping = false;

            player = GameObject.FindGameObjectWithTag("Player").transform;
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
