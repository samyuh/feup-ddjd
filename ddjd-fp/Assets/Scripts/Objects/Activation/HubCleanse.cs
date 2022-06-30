using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCleanse : MonoBehaviour {
    [SerializeField] private ColorManager _colors;

    #region galenaagate
    [SerializeField] private Material mc_crystal;
    [SerializeField] private Material mc_obsidian;

    [SerializeField] private Material companion_crystal;
    #endregion

    #region regionOne
    [SerializeField] private Material i1_grass_road;
    [SerializeField] private Material i2_grass_road;
    [SerializeField] private Material i3_grass_road;

    [SerializeField] private Material i1a3_crystal;

    [SerializeField] private Material i3_obsidian;
    #endregion
    
    #region regionTwo
    [SerializeField] private Material i4_grass_road;
    [SerializeField] private Material i5_grass_road;
    [SerializeField] private Material i6_grass_road;

    [SerializeField] private Material i4a6_crystal;

    [SerializeField] private Material i6_obsidian;
    #endregion

    private float _i;
    private float _obsidian;

    private void Awake() {
        Events.OnCleanZone.AddListener(CleanedZone);

        Events.OnSetActiveCrystal.AddListener(CrystalChange);
    }

    private void CrystalChange(CrystalData data) {
        string crystal = "";

        if (data.id == 0) {
            crystal = "neutral";
        } else if (data.id == 1) {
            crystal = "fire";
        } else if (data.id == 2) {
            crystal = "air";
        }
        

        mc_crystal.SetColor("_Base_color", _colors.getColor(crystal + "_base_color"));
        mc_crystal.SetColor("_Top_color", _colors.getColor(crystal +  "_top_color"));
        mc_crystal.SetColor("_Bottom_color", _colors.getColor(crystal + "_bottom_color"));

        mc_obsidian.SetColor("_Emission_Color", _colors.getColor(crystal + "_emission_color"));
        mc_obsidian.SetColor("_Dissolve_color", _colors.getColor(crystal + "_emission_color"));

        companion_crystal.SetColor("_Base_color", _colors.getColor(crystal + "_base_color"));
        companion_crystal.SetColor("_Top_color", _colors.getColor(crystal + "_top_color"));
        companion_crystal.SetColor("_Bottom_color", _colors.getColor(crystal + "_bottom_color"));
    }   

    private void CleanedZone(int id) {
        if (id == 3) {
            CleanseIsle3();

            Events.GetFire.Invoke();
        } else if (id == 6) {
            CleanseIsle6();

            Events.GetAir.Invoke();
        }
    }

    private void CleanseIsle3() {
        i1a3_crystal.SetColor("_Base_color", _colors.getColor("neutral_base_color"));
        i1a3_crystal.SetColor("_Top_color", _colors.getColor("neutral_top_color"));
        i1a3_crystal.SetColor("_Bottom_color", _colors.getColor("neutral_bottom_color"));

        _i = 0.5f;
        StartCoroutine(Grass3());

        _obsidian = 0f;
        StartCoroutine(Obsidian(i3_obsidian));
    }

     private void CleanseIsle6() {
        i4a6_crystal.SetColor("_Base_color", _colors.getColor("neutral_base_color"));
        i4a6_crystal.SetColor("_Top_color", _colors.getColor("neutral_top_color"));
        i4a6_crystal.SetColor("_Bottom_color", _colors.getColor("neutral_bottom_color"));

        _i = 0.5f;
        StartCoroutine(Grass6());

        _obsidian = 0f;
        StartCoroutine(Obsidian(i6_obsidian));
    }

    IEnumerator Grass3() {
        while (_i <= 1f) { 
            i1_grass_road.SetFloat("_Island1_Saturation", _i);
            i2_grass_road.SetFloat("_Island2_Saturation", _i);
            i3_grass_road.SetFloat("_Island3_Saturation", _i);
            _i += 0.05f;
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator Grass6() {
        while (_i <= 1f) { 
            i4_grass_road.SetFloat("_Island1_Saturation", _i);
            i5_grass_road.SetFloat("_Island2_Saturation", _i);
            i6_grass_road.SetFloat("_Island3_Saturation", _i);
            _i += 0.01f;
            yield return new WaitForSeconds(.05f);
        }
    }

    IEnumerator Obsidian(Material Obsidian) {
        while (_obsidian <= 1f) { 
            Obsidian.SetFloat("_Dissolve_Amount", _obsidian);
            _obsidian += 0.01f;
            yield return new WaitForSeconds(.05f);
        }
    }
}
