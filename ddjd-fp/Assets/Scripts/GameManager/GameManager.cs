using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Game Data
    [SerializeField] private GameData _data;
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
        Events.OnCatchCrystal.AddListener(CatchCrystal);
        Events.OnUseHealthCrystal.AddListener(UseHealthCrystal);
    }

    private void HealthUpdate(int currentHealth, int maxHealth) {
        _data.CurrentHealth = currentHealth;
        _data.MaxHealth = maxHealth;
    }

    private void CatchHealthCrystal() {
        if(_data.HealthCrystal > _data.MaxHealthCrystal) {
            Debug.Log("Can't Carry More Health Crystals");
        } else {
            _data.HealthCrystal += 1;
            Debug.Log("Collected Health Crystal");

        }
    }

    private void CatchCrystal() {
        _data.ManaCrystal += 1;
        Debug.Log("Collected Mana Crystal");
    }

    private void UseHealthCrystal()
    {
        _data.HealthCrystal -= 1;
        Debug.Log("Used Health Crystal");
    }

    public void UpdateCurrentIsland(int number)
    {
        _data.CurrentIsland = number;
    }
    #endregion
}
