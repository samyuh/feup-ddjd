using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour 
{
    private bool _active = false;
    
    #region Health
    [SerializeField] private GameObject _potionInterface;
    private SVGImage[] _potionList;
    private int _potionNum = 0;

    [SerializeField] private Sprite _nothing;
    [SerializeField] private Sprite _health;
    #endregion

    #region Scroll
     [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _elements;

    [SerializeField] private SVGImage _scrollsMap;
    [SerializeField] private SVGImage _scrollsText;
    [SerializeField] private Sprite _scrollSprite;
    #endregion

    #region Crystal
     [SerializeField] private Sprite _airCrystal;
    [SerializeField] private Sprite _fireCrystal;

    [SerializeField] private SVGImage _firstSpot;
    [SerializeField] private SVGImage _secondSpot;
    #endregion

    private void Awake() {   
        gameObject.SetActive(false);

        Events.GetFire.AddListener(FireCrystal);
        Events.GetAir.AddListener(AirCrystal);

        #region Health
        Events.OnCatchHealthCrystal.AddListener(OnCollectHealth);
        Events.OnUseHealthCrystal.AddListener(OnUseHealth);

        _potionList = _potionInterface.GetComponentsInChildren<SVGImage>();
        foreach (SVGImage child in _potionList) {
            child.sprite = _nothing;
        }
        #endregion

        #region Scroll
        Events.OnCatchScroll.AddListener(OnCollectScroll);
        #endregion
    }

    public void FireCrystal() {
        _firstSpot.sprite = _fireCrystal;
    }

    public void AirCrystal() {
        _secondSpot.sprite = _airCrystal;
    }

    public void OnCollectScroll(int id) {
        Debug.Log("here");
        Debug.Log(id);
       if (id == 0) {
        _map.GetComponent<Button>().enabled = true;
        _scrollsMap.sprite = _scrollSprite;
       } else if (id == 1) {
        _elements.GetComponent<Button>().enabled = true;
        _scrollsText.sprite = _scrollSprite;
       }
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