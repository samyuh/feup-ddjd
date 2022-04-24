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
        Events.OnCatchHealthCrystal.AddListener(CatchHealthCrystal);
        Events.OnCatchManaCrystal.AddListener(CatchManaCrystal);
        Events.OnUseHealthCrystal.AddListener(UseHealthCrystal);
        Events.OnUseManaCrystal.AddListener(UseManaCrystal);
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

    private void CatchManaCrystal() {
        _data.ManaCrystal += 1;
        Debug.Log("Collected Mana Crystal");
    }

    private void UseHealthCrystal()
    {
        _data.HealthCrystal -= 1;
        Debug.Log("Used Health Crystal");
    }

    private void UseManaCrystal()
    {
        _data.ManaCrystal -= 1;
        Debug.Log("Used Mana Crystal");
    }
    #endregion
}
