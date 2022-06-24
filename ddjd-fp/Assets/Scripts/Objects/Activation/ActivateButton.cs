using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour {
    public enum ElementType { Neutral, Fire, Air }
    public ElementType element;
    
    private string _strElement; 


    [SerializeField] private ColorManager _colors;
    [SerializeField] private Material _switchCrystal;
    [SerializeField] private GameObject _armature;
    [SerializeField] private GameObject _collider;

    public int portal;
    private bool _alreadyActive = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _distance;
    private float _elapsedTime = 0f;

    void Awake() {
        if (element == ElementType.Fire) {
            _strElement = "fire";
        } else if (element == ElementType.Air) {
            _strElement = "air";
        } else {
            _strElement = "neutral";
        }

        _switchCrystal.SetColor("_Base_color",_colors.getColor("deactivated_crystal_base"));
        _switchCrystal.SetColor("_Top_color", _colors.getColor("deactivated_crystal_base"));
        _switchCrystal.SetColor("_Bottom_color", _colors.getColor("deactivated_crystal_base"));
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player" && !_alreadyActive) {
            Events.OnActivatePortal.Invoke(portal);
             _startPosition = _armature.transform.position;
            _targetPosition =  new Vector3(_armature.transform.position.x, _armature.transform.position.y - 0.15f, _armature.transform.position.z);
            _distance = Vector3.Distance(_startPosition, _targetPosition);
             _alreadyActive = true;
        }
    }

    void Update() {
        if (_alreadyActive) {
            if (_armature.transform.position != _targetPosition) {
                _elapsedTime += Time.deltaTime;
                float distanceDisplacement = _elapsedTime * 0.15f;
                float interpolationRatio = distanceDisplacement / _distance;
                _armature.transform.position = Vector3.Lerp(_startPosition, _targetPosition, interpolationRatio);
                _collider.transform.position = _armature.transform.position;
            } else {
                _switchCrystal.SetColor("_Base_color",_colors.getColor(_strElement + "_base_color"));
                _switchCrystal.SetColor("_Top_color", _colors.getColor(_strElement + "_top_color"));
                _switchCrystal.SetColor("_Bottom_color", _colors.getColor(_strElement + "_bottom_color"));
            } 
        }
    }
}
