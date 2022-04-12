using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    #region Public Data
    [Header("Player")]
    public float MoveSpeed = 2.0f;
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;
    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;
    #endregion

    #region Camera
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    #endregion

    #region GameObjects
    private CharacterController _controller;
    private GameObject _mainCamera;
    private InputHandler _playerInput;
    
    #endregion

    #region Runtime Attributes
    private PlayerState _currentState;
    private PlayerStateFactory _states;

    // Player
    private float _targetSpeed;

    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // Jump
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    #endregion

    #region Getters and Setters
    public CharacterController Controller  { get { return _controller; } set { _controller = value;} }
    public GameObject MainCamera  { get { return _mainCamera; } set { _mainCamera = value;} }
    public InputHandler PlayerInput { get { return _playerInput; } set { _playerInput = value;} }
    public PlayerState CurrentState {get { return _currentState; }  set { _currentState = value; }}

    public float TargetSpeed  {get { return _targetSpeed; }  set { _targetSpeed = value; }}
    public float Speed  {get { return _speed; }  set { _speed = value; }}
    public float TargetRotation  {get { return _targetRotation; }  set { _targetRotation = value; }}
    public float RotationVelocity {get { return _rotationVelocity; }  set { _rotationVelocity = value; }}
    public float VerticalVelocity {get { return _verticalVelocity; }  set { _verticalVelocity = value; }}
    public float TerminalVelocity  {get { return _terminalVelocity; }  set { _terminalVelocity = value; }}
    
    public float JumpTimeoutDelta {get { return _jumpTimeoutDelta; }  set { _jumpTimeoutDelta = value; }}
    public float FallTimeoutDelta  {get { return _fallTimeoutDelta; }  set { _fallTimeoutDelta = value; }}
    #endregion

    private void Awake() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _controller = GetComponent<CharacterController>();
        _playerInput = GetComponent<InputHandler>();

        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Update() {
        _currentState.UpdateStates();

        CalculateSpeed();
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void LateUpdate() {
		CameraRotation();
	}

    #region Camera
    private void CameraRotation() {
        if (_playerInput.look.sqrMagnitude >= _threshold && !LockCameraPosition) {
            _cinemachineTargetYaw += _playerInput.look.x;
            _cinemachineTargetPitch += _playerInput.look.y;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float ifAngle, float ifMin, float ifMax) {
        if (ifAngle < -360f) ifAngle += 360f;
        if (ifAngle > 360f) ifAngle -= 360f;
        return Mathf.Clamp(ifAngle, ifMin, ifMax);
    }
    #endregion

    #region Player State Functions
    public void GroundedCheck() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    public void CalculateSpeed() {
        float speedOffset = 0.1f;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        
        if (currentHorizontalSpeed < _targetSpeed - speedOffset || currentHorizontalSpeed > _targetSpeed + speedOffset) {
            _speed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = _targetSpeed;
        }
    }

  public void Move() {
        Vector3 inputDirection = new Vector3(_playerInput.move.x, 0.0f, _playerInput.move.y).normalized;
        if (_playerInput.move != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
    #endregion 
}
