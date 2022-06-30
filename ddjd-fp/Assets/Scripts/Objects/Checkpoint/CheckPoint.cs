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
        Vector3 position = GetActiveCheckPointPosition();

        if (position != new Vector3(0,0,0)) {
            player.transform.position = new Vector3(position.x + 3f, position.y + 2f, position.z);
        }
        else {
            player.transform.position = new Vector3(20, 52, -3);
        }

        RestoreState();
    }

    #region Static Functions
    public static Vector3 GetActiveCheckPointPosition() {
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)  {
            foreach (GameObject cp in CheckPointsList) {
                if (cp.GetComponent<CheckPoint>()._activated) {
                    result = cp.transform.position;
                    break;
                }
            }
        }
        return result;
    }
    #endregion

    public void ActivateCheckPoint() {
        foreach (GameObject cp in CheckPointsList) {
            cp.GetComponent<CheckPoint>()._activated = false;
        }

        _saveCrystal.SetFloat("_Transparency", 1f);
        _saveEmission.SetColor("_EmissionColor", _colors.getColor("neutral_emission_color"));

        _activated = true;
    }


    private void RestoreState() {
        player.GetComponent<Player>().Data.CurrentHealth = 700;
        player.GetComponent<Player>().Data.ManaCrystal = 700;

        Events.OnHealthUpdate.Invoke(700, 700);
        Events.OnCrystalManaUpdate.Invoke(700, 700);
    }
}