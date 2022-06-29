using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPoint : MonoBehaviour 
{
    public ColorManager _colors;
    public GameData data;

    public bool _activated = false;
    private GameObject player;
    
    [SerializeField] private Material _saveCrystal;
    [SerializeField] private Material _saveEmission;

    public static List<GameObject> CheckPointsList;

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
    public static Vector3 GetActiveCheckPointPosition() {
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)  {
            foreach (GameObject cp in CheckPointsList) {
                if (cp.GetComponent<CheckPoint>()._activated)
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
            cp.GetComponent<CheckPoint>()._activated = false;
            // cp.GetComponent<Animator>().SetBool("Active", false);
        }

        _saveCrystal.SetFloat("_Transparency", 1f);
        _saveEmission.SetColor("_EmissionColor", _colors.getColor("neutral_emission_color"));

        _activated = true;
        Debug.Log("Checkpoint _activated");

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