using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour 
{
    #region Public Variables
    public bool Activated = false;
    #endregion

    #region Private Variables
    // private Animator thisAnimator;
    #endregion

    #region Static Variables
    public static List<GameObject> CheckPointsList;
    #endregion

    void Start()  {
        // thisAnimator = GetComponent<Animator>();

        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
        
        foreach( GameObject x in CheckPointsList) {
            Debug.Log("X elements");
            Debug.Log( x.name);
            Debug.Log("Checkpoint: " + x.GetComponent<CheckPoint>().Activated);
        }

        Events.OnDeath.AddListener(OnDeathRespawn);
    }

    public void OnDeathRespawn() {
        Debug.Log("Morreu");
    }

    #region Static Functions
    // Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition() {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)  {
            foreach (GameObject cp in CheckPointsList) {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }
    #endregion

    #region Private Functions
    // Activate the checkpoint
    public void ActivateCheckPoint() {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList) {
            cp.GetComponent<CheckPoint>().Activated = false;
            // cp.GetComponent<Animator>().SetBool("Active", false);
        }

        // We activated the current checkpoint
        Activated = true;
        // thisAnimator.SetBool("Active", true);
        Debug.Log("Checkpoint Activated");

        // foreach (GameObject cp in CheckPointsList)
        // {
        //     Degub.Log(cp.transform.position);
        // }
    }
    #endregion

    // void OnTriggerEnter(Collider other)
    // {
    //     // If the player passes through the checkpoint, we activate it
    //     if (other.tag == "Player")
    //     {
    //         ActivateCheckPoint();
    //     }
    // }
}