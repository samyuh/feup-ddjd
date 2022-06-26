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

    private string _walkSoundEvent;
    private string _jumpSoundEvent;
    private string _jumpFallSoundEvent;
    private string _deathSoundEvent;
    private string _hitSoundEvent;
    private string _healSoundEvent;
    private string _lightAttackSoundEvent;
    private string _mediumAttackSoundEvent;
    private string _heavyAttackSoundEvent;
    private string _dodgeSoundEvent;
    private string _blockSoundEvent;


    public Animator Animator {get { return _animator; } set { _animator = value;}}
    public CharacterController Controller  {get { return _controller; } set { _controller = value;}}
    public InputHandler PlayerInput {get { return _playerInput; } set { _playerInput = value;}}

    public string WalkSoundEvent {get { return _walkSoundEvent; } set { _walkSoundEvent = value;}}
    public string JumpSoundEvent {get { return _jumpSoundEvent; } set { _jumpSoundEvent = value;}}
    public string JumpFallSoundEvent {get { return _jumpFallSoundEvent; } set { _jumpFallSoundEvent = value;}}
    public string LightAttackSoundEvent {get { return _lightAttackSoundEvent; } set { _lightAttackSoundEvent = value;}}
    public string MediumAttackSoundEvent {get { return _mediumAttackSoundEvent; } set { _mediumAttackSoundEvent = value;}}
    public string HeavyAttackSoundEvent {get { return _heavyAttackSoundEvent; } set { _heavyAttackSoundEvent = value;}}
    public string DodgeSoundEvent {get { return _dodgeSoundEvent; } set { _dodgeSoundEvent = value;}}
    public string BlockSoundEvent {get { return _blockSoundEvent; } set { _blockSoundEvent = value;}}

    #endregion

    #region State Machine
    public StateMachine StateMachine;
    public StateFactory StateFactory;
    #endregion

    #region Interactable Items
    public CrystalData ActiveCrystal = null;
    public GameObject InteractableItem = null;
    public GameObject SecondaryObjectToThrow = null;

    #endregion

    private void Awake() {
        // External Components needed
        _camera = new PlayerCamera();
        _playerInput = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputHandler>();

        // Player Controller
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _data = new PlayerData();

        // Player Sounds
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Terrain", 1);

        _walkSoundEvent = "event:/Footsteps";
        _jumpSoundEvent = "event:/jump";
        _jumpFallSoundEvent = "event:/jumpFall";
        _deathSoundEvent = "event:/galena_death";
        _hitSoundEvent = "event:/galena_hitreact";
        _healSoundEvent = "event:/galena_heal";
        _lightAttackSoundEvent = "event:/galena_lightattack";
        _mediumAttackSoundEvent = "event:/galena_mediumattack";
        _heavyAttackSoundEvent = "event:/galena_heavyattack";
        _dodgeSoundEvent = "event:/galena_dodge";
        _blockSoundEvent = "event:/galena_block";

        // State Machine
        StateMachine = new StateMachine(this);
        StateFactory = new StateFactory(this, StateMachine);
        StateMachine.Initialize(StateFactory.IdleState);

        // Crystal
        Events.OnSetActiveCrystal.AddListener(SetActiveCrystal);
    }

    private void SetActiveCrystal(CrystalData newCrystal) {
        ActiveCrystal = newCrystal;
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

    public GameObject InstantiateObj(GameObject prefab, Vector3 second, Quaternion third) {
        return Instantiate(prefab, second, third);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        StateMachine.CurrentState.OnControllerColliderHit(hit);
    }

    public void ApplyDamage(int damage) {
        _data.CurrentHealth -= damage;
        
        if (_data.CurrentHealth <= 0) {
            Events.OnDeath.Invoke();
            // death sound
            FMODUnity.RuntimeManager.PlayOneShot(_deathSoundEvent);
        }
        else {
            Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
            // damage sound
            FMODUnity.RuntimeManager.PlayOneShot(_hitSoundEvent);
        }
    }


    public void GetItem(int item) {
        if(item == 0 && _data.HealthCrystal < _data.MaxHealthCrystal) {
            _data.HealthCrystal += 1;
            print("Health Crystal: " + _data.HealthCrystal);
        }
        else if(item == 1) {
            _data.ManaCrystal += 1;
            print("Mana Crystal: " + _data.ManaCrystal);
        }
    }

    public void ApplyHealth(int health) {
        if(_data.HealthCrystal > 0) {
            if ((_data.CurrentHealth + health) < _data.MaxHealth)
            {
                _data.CurrentHealth += health;
                Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
            }
            else
            {
                _data.CurrentHealth = _data.MaxHealth;
                Events.OnHealthUpdate.Invoke(_data.CurrentHealth, _data.MaxHealth);
            }
            _data.HealthCrystal -= 1;

            // heal sound
            FMODUnity.RuntimeManager.PlayOneShot(_healSoundEvent);

        } else {
            print("No Health Crystals");
        }
    }

    public void DestroyObject(GameObject otherObject){
        Destroy(otherObject);
    }
}
