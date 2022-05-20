using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggleCamera : MonoBehaviour {
    private Animator _currentState;
    private bool _isAiming = false;

    void Start() {
        _currentState = GetComponent<Animator>();
        Events.OnToggleAim.AddListener(OnToggleAim);
    }

    private void OnToggleAim() {
        _isAiming = !_isAiming;
        
        _currentState.SetBool("AimState", _isAiming);
    }
}
