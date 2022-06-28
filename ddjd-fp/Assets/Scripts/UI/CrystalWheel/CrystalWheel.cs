using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VectorGraphics;

public class CrystalWheel: MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private SVGImage itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    private CrystalData crystal;
    private bool _selected;

    private void Awake()
    {   
        _selected = false;
        crystal = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().currentCrystals[id];
        itemImage.sprite = crystal.icon;

        Events.OnChangeCrystalSlots.AddListener(OnChangeCrystalSlots);
    }

    private void OnChangeCrystalSlots() {
        crystal = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().currentCrystals[id];
        itemImage.sprite = crystal.icon;
    }

    private void Update() {
        if (_selected) {
            itemText.text = crystal.name;
        }
    }

    public void Select() {
        _selected = true;
        Events.OnSetActiveCrystal.Invoke(crystal);
    }

    public void Unselect() {
        _selected = false;
    }

    public void HoveEnter() {
        itemText.text = crystal.name;
        Debug.Log(itemImage.sprite);
        Vector3 scale = new Vector3( 1, 1, 1f );
        itemImage.rectTransform.localScale = scale;
    }

    public void HoveExit() {
        itemText.text = "";
        Vector3 scale = new Vector3( 0.55f, 0.55f, 1f );
        itemImage.rectTransform.localScale = scale;
    }
}