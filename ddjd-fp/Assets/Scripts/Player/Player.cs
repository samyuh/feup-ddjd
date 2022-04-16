using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private GameObject _mainCamera;
    public GameObject MainCamera  {get { return _mainCamera; } set { _mainCamera = value;}}

    #region Game Objects
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private Animator _animator;
    private CharacterController _controller;
    private InputHandler _playerInput;

    public PlayerSettings PlayerSettings {get { return _playerSettings; } set { _playerSettings = value;}}
    public Animator Animator {get { return _animator; } set { _animator = value;}}
    public CharacterController Controller  {get { return _controller; } set { _controller = value;}}
    public InputHandler PlayerInput {get { return _playerInput; } set { _playerInput = value;}}
    #endregion

    #region State Machine
    public StateMachine StateMachine;
    public StateFactory StateFactory;
    #endregion

    #region Runtime Attributes
    // Player
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    public float Speed  {get { return _speed; }  set { _speed = value; }}
    public float TargetRotation  {get { return _targetRotation; }  set { _targetRotation = value; }}
    public float RotationVelocity {get { return _rotationVelocity; }  set { _rotationVelocity = value; }}
    public float VerticalVelocity {get { return _verticalVelocity; }  set { _verticalVelocity = value; }}
    public float TerminalVelocity  {get { return _terminalVelocity; }  set { _terminalVelocity = value; }}
    #endregion

    #region Player Status
    private int _currentHealth = 700;
    private int _maxHealth = 700;
    #endregion

    private void Awake() {
        // External Components needed
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();

        // Player Controller
        _controller = GetComponent<CharacterController>();

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

    #region Health
    public void ApplyDamage(int damage) {
        _currentHealth -= damage;

        if (_currentHealth < 0) Events.OnDeath.Invoke();
        else Events.OnHealthUpdate.Invoke(_currentHealth, _maxHealth);
    }
    #endregion

    #region Player State
    public void DestroyObject(GameObject otherObject){
        Destroy(otherObject);
    }

    public bool GroundedCheck() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _playerSettings.GroundedOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, _playerSettings.GroundedRadius, _playerSettings.GroundLayers, QueryTriggerInteraction.Ignore);
    }

    public void Move(float targetSpeed) {
        if (targetSpeed != 0) {
            Vector3 inputDirection = new Vector3(_playerInput.move.x, 0.0f, _playerInput.move.y).normalized;

            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _playerSettings.RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        
        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * _playerSettings.SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = targetSpeed;
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    #endregion 
}
