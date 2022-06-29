using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints = 350;
    [SerializeField] private Slider _healthBar;
    
    public GameObject Spider;
    public GameObject Player;

    #region Animator
    private Animator _animator;
    public Animator Animator {get { return _animator; } set { _animator = value;}}
    #endregion

    private FMOD.Studio.EventInstance _runSoundInstance;
    //public FMOD.Studio.EventInstance _runSoundInstance {get { return _runSoundInstance; } set { _runSoundInstance = value;}}

    private EnemyStateMachine _stateMachine;
    
    #region State Machine
    public EnemyStateMachine StateMachine;
    public EnemyFactory StateFactory;
    #endregion

    private void Start() {
        _healthPoints = 350;
        _animator = GetComponent<Animator>();
        //_runSoundEvent = FMODUnity.RuntimeManager.CreateInstance("event:/spidermob_run");

        //_runSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/spidermob_run");
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(_runSoundInstance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        //_runSoundEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_context.gameObject.transform));
        //_runSoundInstance.start();
        
        // State Machine
        StateMachine = new EnemyStateMachine(this);
        StateFactory = new EnemyFactory(this, StateMachine);
        StateMachine.Initialize(StateFactory.SpiderWalk);
    }

    private void Update() {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Receive Damage
    public void ApplyDamage(int damage) {
        _healthPoints -= damage;
        if (_healthPoints < 0) Death();
        else {
            _healthBar.value = _healthPoints / (float) 350;
        }
    }

    public void Death() {
         _healthBar.value = 0;
        StateMachine.ChangeState(StateFactory.SpiderDeath);
        _runSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _runSoundInstance.release();
    }

    public void Destroy() {
        Destroy(gameObject);
    }
    #endregion
}
