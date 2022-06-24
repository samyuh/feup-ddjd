using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour {
    [SerializeField] private ColorManager _colors;
    [SerializeField] private Material _switchCrystal;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _collider;

    public int portal;
    private bool _alreadyActive = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _distance;
    private float _elapsedTime = 0f;

    void Awake() {
        _switchCrystal.SetColor("_Base_color",_colors.getColor("deactivated_crystal_base"));
        _switchCrystal.SetColor("_Top_color", _colors.getColor("deactivated_crystal_base"));
        _switchCrystal.SetColor("_Bottom_color", _colors.getColor("deactivated_crystal_base"));
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player" && !_alreadyActive) {
            Events.OnActivatePortal.Invoke(portal);
             _startPosition = _button.transform.position;
            _targetPosition =  new Vector3(_button.transform.position.x, _button.transform.position.y - 0.15f, _button.transform.position.z);
            _distance = Vector3.Distance(_startPosition, _targetPosition);
             _alreadyActive = true;

            //ColorSwatch result = _colors.ColorList.Find(x => x.Name == "neutral_base_color");
            //Debug.Log(_colors.getColor("neutral_base_color"));



            /*
            deactivated_crystal_base > neutral_base_color
            Top color  _Top_color
            deactivated_crystal_top > neutral_top_color
            Bottom color  _Bottom_color
            deactivated_crystal_bottom > neutral_bottom_color
            */

        }
    }

    void Update() {
        if (_alreadyActive) {
            if (_button.transform.position != _targetPosition) {
                // Distance moved equals elapsed time times speed
                _elapsedTime += Time.deltaTime;
                float distanceDisplacement = _elapsedTime * 0.15f;
                float interpolationRatio = distanceDisplacement / _distance;
                _button.transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolationRatio);
                _collider.transform.position = _button.transform.position;
            } else {
                _switchCrystal.SetColor("_Base_color",_colors.getColor("neutral_base_color"));
                _switchCrystal.SetColor("_Top_color", _colors.getColor("neutral_top_color"));
                _switchCrystal.SetColor("_Bottom_color", _colors.getColor("neutral_bottom_color"));
            } 
        }
    }
}
