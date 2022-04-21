using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    #region Game Data
    [SerializeField] private GameData _data;
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

        EnablePlayer();
    }

    #region Player
    private void EnablePlayer() {
        Events.OnHealthUpdate.AddListener(HealthUpdate);
        Events.OnCatchHealthPlant.AddListener(CatchHealthPlant);
        Events.OnCatchCrystal.AddListener(CatchCrystal);
    }

    private void HealthUpdate(int currentHealth, int maxHealth) {
        _data.CurrentHealth = currentHealth;
        _data.MaxHealth = maxHealth;
    }

    private void CatchHealthPlant() {
       _data.NumHealthPlants += 1;
       Debug.Log("Collected Health Plant");
    }

    private void CatchCrystal() {
        _data.NumCrystals += 1;
        Debug.Log("Collected Crystal");
    }
    #endregion
}
