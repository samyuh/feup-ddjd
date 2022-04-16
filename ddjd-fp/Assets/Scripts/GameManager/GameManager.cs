using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    #region Game Data
    [SerializeField] private GameData _data;
    #endregion

    #region Camera
    private GameObject _mainCamera;
    #endregion

    #region Player
    private GameObject _player;
    #endregion

    #region Input
    private InputHandler _input;
    public InputHandler Input { get { return _input; } set { _input = value;} }
    #endregion

    private void Awake() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _player = GameObject.FindGameObjectWithTag("Player");
        _input = GetComponent<InputHandler>();

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
}
