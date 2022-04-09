using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
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

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    //private PlayerInput _playerInput;
    private Animator _animator;
    private CharacterController _controller;
    private InputHandler _input;
    private GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private bool _hasAnimator;

    private void Awake() {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Start is called before the first frame update
    void Start() {
        //_hasAnimator = TryGetComponent(out _animator);
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<InputHandler>();
       //_playerInput = GetComponent<PlayerInput>();

        //AssignAnimationIDs();

        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    // Update is called once per frame
    void Update() {
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void LateUpdate() {
		CameraRotation();
	}

    #region Camera
    private void CameraRotation() {
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition) {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;
            
            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    #endregion

    #region BasicMovement
    private void JumpAndGravity() {
        if (Grounded) {
            _fallTimeoutDelta = FallTimeout;
            if (_verticalVelocity < 0.0f) {
                _verticalVelocity = -2f;
            }

            if (_input.jump && _jumpTimeoutDelta <= 0.0f)  {
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
            if (_jumpTimeoutDelta >= 0.0f) {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else  {
            _jumpTimeoutDelta = JumpTimeout;

            if (_fallTimeoutDelta >= 0.0f) {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            _input.jump = false;
        }

        if (_verticalVelocity < _terminalVelocity) {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void GroundedCheck() {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move() {
        float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset) {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else {
            _speed = targetSpeed;
        }
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);


        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        if (_input.move != Vector2.zero) {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    #endregion 
}
