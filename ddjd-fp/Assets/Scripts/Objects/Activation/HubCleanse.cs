using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubCleanse : MonoBehaviour {
    [SerializeField] private ColorManager _colors;

    #region regionOne
    [SerializeField] private Material i1_grass_road;
    [SerializeField] private Material i2_grass_road;
    [SerializeField] private Material i3_grass_road;

    [SerializeField] private Material i1a3_crystal;

    [SerializeField] private Material i3_obsidian;

    private float _i;
    #endregion
    
    #region regionTwo
    [SerializeField] private Material i4_grass_road;
    [SerializeField] private Material i5_grass_road;
    [SerializeField] private Material i6_grass_road;

    [SerializeField] private Material i4a6_crystal;

    [SerializeField] private Material i6_obsidian;

    #endregion
    private void Awake() {
        CleanseIsle3();
    }

    private void CleanseIsle3() {
        i1a3_crystal.SetColor("_Base_color", _colors.getColor("neutral_base_color"));
        i1a3_crystal.SetColor("_Top_color", _colors.getColor("neutral_top_color"));
        i1a3_crystal.SetColor("_Bottom_color", _colors.getColor("neutral_bottom_color"));

        _i = 0f;
        Debug.Log(_i);
        StartCoroutine(Grass3());

        i3_obsidian.SetFloat("_Dissolve_Amount", 1f);
    }

     private void CleanseIsle6() {
        i4a6_crystal.SetColor("_Base_color", _colors.getColor("neutral_base_color"));
        i4a6_crystal.SetColor("_Top_color", _colors.getColor("neutral_top_color"));
        i4a6_crystal.SetColor("_Bottom_color", _colors.getColor("neutral_bottom_color"));

        _i = 0f;
        StartCoroutine(Grass6());

        i6_obsidian.SetFloat("_Dissolve_Amount", 1f);
    }

    IEnumerator Grass3() {
        while (_i <= 1f) { 
            Debug.Log(_i);
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
            _i += 0.05f;
            yield return new WaitForSeconds(.2f);
        }
    }
}
