using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private GameObject _mainCamera;
    public GameObject MainCamera  {get { return _mainCamera; } set { _mainCamera = value;}}

    #region Game Objects
    private PlayerData _data;
    private Animator _animator;
    private CharacterController _controller;
    private InputHandler _playerInput;

    public PlayerData Data {get { return _data; } set { _data = value;}}
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
        PlayerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();

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

    #region Health
    public void ApplyDamage(int damage) {
        _data.CurrentHealth -= damage;

        if (_data.CurrentHealth < 0) Events.OnDeath.Invoke();
        else Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
    }
    #endregion

    #region Player State
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

    public bool GroundedCheck() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _data.GroundedOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, _data.GroundedRadius, _data.GroundLayers, QueryTriggerInteraction.Ignore);
    }

    public void Move(float targetSpeed) {
        if (targetSpeed != 0) {
            Vector3 inputDirection = new Vector3(_playerInput.move.x, 0.0f, _playerInput.move.y).normalized;

            _data.TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            
            float rotationVelocity = _data.RotationVelocity;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _data.TargetRotation, ref rotationVelocity, _data.RotationSmoothTime);
            _data.RotationVelocity = rotationVelocity;
            
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) {
            _data.Speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _data.SpeedChangeRate);
            _data.Speed = Mathf.Round(_data.Speed * 1000f) / 1000f;
        }
        else {
            _data.Speed = targetSpeed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _data.TargetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_data.Speed * Time.deltaTime) + new Vector3(0.0f, _data.VerticalVelocity, 0.0f) * Time.deltaTime);
    }
    #endregion 
}
