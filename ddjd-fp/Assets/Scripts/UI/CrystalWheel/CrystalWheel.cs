using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrystalWheel: MonoBehaviour {
    public int id;
    public string itemName;
    public TextMeshProUGUI itemText;
    private bool _selected = false;

    private void Update() {
        if (_selected) {
            itemText.text = itemName;
        }
    }

    public void Select() {
        _selected = true;
        Events.OnChangeSelectedCrystal.Invoke(id);
    }

    public void Unselect() {
        _selected = false;
    }

    public void HoveEnter() {
        itemText.text = itemName;
    }

    public void HoveExit() {
        itemText.text = "";
    }
}