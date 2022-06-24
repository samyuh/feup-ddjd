using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour 
{
    private bool _active = false;
    
    [SerializeField] private GameObject _crystalInterface;
    [SerializeField] private GameObject _potionInterface;

    private SVGImage[] _crystalList;
    private SVGImage[] _potionList;

    private int _crystalNum = 0;
    private int _potionNum = 0;

    // TODO: REFACTOR THIS
    [SerializeField] private Sprite _nothing;
    [SerializeField] private Sprite _airCrystal;
    [SerializeField] private Sprite _health;

    private void Awake()
    {   
        gameObject.SetActive(false);

        Events.OnCatchCrystal.AddListener(OnCollectCrystal);

        #region Health
        Events.OnCatchHealthCrystal.AddListener(OnCollectHealth);
        Events.OnUseHealthCrystal.AddListener(OnUseHealth);

        _potionList = _potionInterface.GetComponentsInChildren<SVGImage>();
        foreach (SVGImage child in _potionList) {
            child.sprite = _nothing;
        }
        #endregion
    }

    public void OnCollectCrystal() {
       
    }

    public void OnCollectHealth() {
        _potionList[_potionNum].sprite = _health;
        _potionNum++;
    }

    public void OnUseHealth() {
        if (_potionNum > 0) {
            _potionNum--;
            _potionList[_potionNum].sprite = _nothing;
        }
    }

    public void OnToggleInventory() 
    {
        _active = !_active;
        Cursor.lockState = _active ? CursorLockMode.None : CursorLockMode.Locked;
        gameObject.SetActive(_active);

        if(_active) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }

    }
}