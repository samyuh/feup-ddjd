using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalListener : MonoBehaviour {
    public enum ElementType { Fire, Air, Neutral }
    public ElementType element;
    
    private string _strElement; 

    [SerializeField] private ColorManager _colors;

    [SerializeField] private int id;
    [SerializeField] private int _numberButton;

    [SerializeField] private Material _crystalPortal;
    [SerializeField] private Material _stonePortal;
    [SerializeField] private Material _passagePortal;

    [SerializeField] public GameObject Teleporter;

    public void Awake() {
        if (element == ElementType.Fire) {
            _strElement = "fire";
        } else if (element == ElementType.Air) {
            _strElement = "air";
        } else  if (element == ElementType.Neutral) {
            _strElement = "neutral";
        }
        /*
        _crystalPortal.SetColor("_Base_color", _colors.getColor("deactivated_crystal_base"));
        _crystalPortal.SetColor("_Top_color", _colors.getColor("deactivated_crystal_top"));
        _crystalPortal.SetColor("_Bottom_color", _colors.getColor("deactivated_crystal_bottom"));
        _stonePortal.SetColor("_Emission_Color", _colors.getColor("deactivated_emission"));
        _passagePortal.SetFloat("_Dissolve_Amount", 50f);
        */

        Events.OnActivatePortal.AddListener(ActivatePortal);
        Events.OnDeactivatePortal.AddListener(DeactivatePortal);
    }

    private void ActivatePortal(int portalNumber) {
        if (portalNumber == id) {
            _numberButton -= 1;

            if (_numberButton <= 0) {
                _crystalPortal.SetColor("_Base_color",_colors.getColor(_strElement +"_base_color"));
                _crystalPortal.SetColor("_Top_color", _colors.getColor(_strElement +"_top_color"));
                _crystalPortal.SetColor("_Bottom_color", _colors.getColor(_strElement +"_bottom_color"));
                _stonePortal.SetColor("_Emission_Color", _colors.getColor(_strElement +"_emission_color"));
                _passagePortal.SetFloat("_Dissolve_Amount", 0.5f);
                Teleporter.SendMessage("Activate");
            }
        }
    }

    private void DeactivatePortal(int portalNumber) {
        if (portalNumber == id) {
            _crystalPortal.SetColor("_Base_color",_colors.getColor("deactivated_crystal_base"));
            _crystalPortal.SetColor("_Top_color", _colors.getColor("deactivated_crystal_top"));
            _crystalPortal.SetColor("_Bottom_color", _colors.getColor("deactivated_crystal_bottom"));
            _stonePortal.SetColor("_Emission_Color", _colors.getColor("deactivated_emission"));
            _passagePortal.SetFloat("_Dissolve_Amount", 50f);
            Teleporter.SendMessage("Deactivate");
        }
    }
}
