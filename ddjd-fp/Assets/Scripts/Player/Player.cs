using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private GameObject _mainCamera;
    public GameObject MainCamera  {get { return _mainCamera; } set { _mainCamera = value;}}

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

    private void Awake() {
        // External Components needed
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
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

    public void ApplyDamage(int damage) {
        _data.CurrentHealth -= damage;

        if (_data.CurrentHealth < 0) Events.OnDeath.Invoke();
        else Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
    }

    public void DestroyObject(GameObject otherObject){
        Destroy(otherObject);
    }
    
    public void OnTriggerStay(Collider otherObject) {
        StateMachine.CurrentState.OnTriggerStay(otherObject);

        // TODO: 
        // Create a Substate to catch
        // Instead of collider, raycast a sphere
        if (_playerInput.interact) {
            if (otherObject.gameObject.tag == "Health") {
                Events.OnCatchHealthPlant.Invoke();
                Destroy(otherObject.gameObject);
            } 
            else if (otherObject.gameObject.tag == "Crystal") {
                Events.OnCatchCrystal.Invoke();
                Destroy(otherObject.gameObject);
            }
        }
    }
}
