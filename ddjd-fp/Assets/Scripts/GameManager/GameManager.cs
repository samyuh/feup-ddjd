using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Game Data
    [SerializeField] private GameData _data;
    [SerializeField] private  CrystalData _airCrystal;
    [SerializeField] private CrystalData _fireCrystal;
    [SerializeField] public List<CrystalData> currentCrystals;
    #endregion

    #region Player
    private Player _player;
    #endregion

    #region Input
    private InputHandler _input;
    #endregion


    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _input = GetComponent<InputHandler>();
        
        SceneManager.LoadScene("Isle 1", LoadSceneMode.Additive);
        SceneManager.LoadScene("Isle 2", LoadSceneMode.Additive);
        SceneManager.LoadScene("Isle 3", LoadSceneMode.Additive);
        SceneManager.LoadScene("Isle 4", LoadSceneMode.Additive);
        SceneManager.LoadScene("Isle 5", LoadSceneMode.Additive);
        SceneManager.LoadScene("Isle 6", LoadSceneMode.Additive);

        EnablePlayer(); 
    }

    #region Player
    private void EnablePlayer() {
        Events.OnHealthUpdate.AddListener(HealthUpdate);
        Events.OnCatchHealthCrystal.AddListener(CatchHealthCrystal);
        Events.OnUseHealthCrystal.AddListener(UseHealthCrystal);

        Events.GetFire.AddListener(FireCrystal);
        Events.GetAir.AddListener(AirCrystal);
    }

    private void HealthUpdate(int currentHealth, int maxHealth) {
        _data.CurrentHealth = currentHealth;
        _data.MaxHealth = maxHealth;
    }

    private void CatchHealthCrystal() {
        if(_data.HealthCrystal < _data.MaxHealthCrystal) {
            _data.HealthCrystal += 1;
        }
    }

    private void FireCrystal() {
        currentCrystals[1] = _fireCrystal;

        Debug.Log("Here");
    }

    private void AirCrystal() {
        currentCrystals[2] = _airCrystal;
    }

    private void UseHealthCrystal() {
        _data.HealthCrystal -= 1;
    }

    public void UpdateCurrentIsland(int number)
    {
        _data.CurrentIsland = number;
    }
    #endregion
}
