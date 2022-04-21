using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private PlayerCamera _camera;
    public PlayerCamera Camera {get { return _camera; } set { _camera = value;}}

    private PlayerData _data;
    public PlayerData Data {get { return _data; } set { _data = value;}}

    #region Game Objects
    private Animator _animator;
    private CharacterController _controller;
    private InputHandler _playerInput;

    public Animator Animator {get { return _animator; } set { _animator = value;}}
    public CharacterController Controller  {get { return _controller; } set { _controller = value;}}
    public InputHandler PlayerInput {get { return _playerInput; } set { _playerInput = value;}}
    #endregion

    #region State Machine
    public StateMachine StateMachine;
    public StateFactory StateFactory;
    #endregion

    #region Interactable Items
    public GameObject InteractableItem = null;
    #endregion

    private void Awake() {
        // External Components needed
        _camera = new PlayerCamera();
        _playerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();

        // Player Controller
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _data = new PlayerData();

        // State Machine
        StateMachine = new StateMachine(this);
        StateFactory = new StateFactory(this, StateMachine);
        StateMachine.Initialize(StateFactory.IdleState);
    }

    private void Update() {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void LateUpdate() {
        _camera.LateUpdateCamera( _playerInput.Look.sqrMagnitude,  _playerInput.Look.x, _playerInput.Look.y);
    }

    private void OnTriggerEnter(Collider otherObject) {
        InteractableItem = otherObject.gameObject;
    }

    private void OnTriggerExit(Collider otherObject) {
        InteractableItem = null;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        StateMachine.CurrentState.OnControllerColliderHit(hit);
    }

    public void ApplyDamage(int damage) {
        _data.CurrentHealth -= damage;

        if (_data.CurrentHealth < 0) Events.OnDeath.Invoke();
        else Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
    }

    public void DestroyObject(GameObject otherObject){
        Destroy(otherObject);
    }
}
