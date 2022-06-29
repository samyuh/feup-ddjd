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
    GameObject player;
    [SerializeField] private GameData data;
    #endregion

    #region Static Variables
    public static List<GameObject> CheckPointsList;
    #endregion

    void Start()  {
        player = GameObject.Find("Player");
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint").ToList();
        Events.OnDeath.AddListener(OnDeathRespawn);
    }

    public void OnDeathRespawn() {
        Debug.Log("Morreu");
        Vector3 position = GetActiveCheckPointPosition();

        if(position != new Vector3(0,0,0)){
            player.transform.position = position;
            RestoreState();
        }
        else{
            Debug.Log("No Active Checkpoints!!");
        }
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
    public void ActivateCheckPoint() {
        foreach (GameObject cp in CheckPointsList) {
            cp.GetComponent<CheckPoint>().Activated = false;
            // cp.GetComponent<Animator>().SetBool("Active", false);
        }

        // We activated the current checkpoint
        Activated = true;
        // thisAnimator.SetBool("Active", true);
        Debug.Log("Checkpoint Activated");

        StoreState();

    }
    #endregion

    private void StoreState() {
        // Debug.Log(_data.Cu   rrentHealth);

    }

    private void RestoreState(){
        // _data = data;
    }
   
}