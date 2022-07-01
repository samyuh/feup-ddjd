using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour {
    protected int _healthPoints = 350;
    [SerializeField] private Slider _healthBar;
    
    public GameObject Spider;

    #region Animator
    private Animator _animator;
    public Animator Animator {get { return _animator; } set { _animator = value;}}
    #endregion

    private SpiderStateMachine _stateMachine;
    
    #region State Machine
    public SpiderStateMachine StateMachine;
    public SpiderFactory StateFactory;
    #endregion

    private void Start() {
        _healthPoints = 350;
        _animator = GetComponent<Animator>();

        
        // State Machine
        StateMachine = new SpiderStateMachine(this);
        StateFactory = new SpiderFactory(this, StateMachine);
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
        Destroy(transform.Find("Canvas").gameObject);
        StateMachine.ChangeState(StateFactory.SpiderDeath);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
    #endregion
}
