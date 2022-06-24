using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalListener : MonoBehaviour {
    [SerializeField] private ColorManager _colors;

    [SerializeField] private int id;
    [SerializeField] private int _numberButton;

    [SerializeField] private Material _crystalPortal;
    [SerializeField] private Material _stonePortal;
    [SerializeField] private Material _passagePortal;

    public void Awake() {
            _crystalPortal.SetColor("_Base_color",_colors.getColor("deactivated_crystal_base"));
            _crystalPortal.SetColor("_Top_color", _colors.getColor("deactivated_crystal_top"));
            _crystalPortal.SetColor("_Bottom_color", _colors.getColor("deactivated_crystal_bottom"));

            _stonePortal.SetColor("_Emission_Color", _colors.getColor("deactivated_emission"));

            _passagePortal.SetFloat("_Dissolve_Amount", 50f);

        Events.OnActivatePortal.AddListener(ActivatePortal);
    }

    private void ActivatePortal(int portalNumber) {
        if (portalNumber == id) {
            _numberButton -= 1;

            if (_numberButton == 0) {
                _crystalPortal.SetColor("_Base_color",_colors.getColor("fire_base_color"));
                _crystalPortal.SetColor("_Top_color", _colors.getColor("fire_top_color"));
                _crystalPortal.SetColor("_Bottom_color", _colors.getColor("fire_bottom_color"));

                _stonePortal.SetColor("_Emission_Color", _colors.getColor("fire_emission_color"));

                _passagePortal.SetFloat("_Dissolve_Amount", 0.5f);
            }
        }
    }
}
