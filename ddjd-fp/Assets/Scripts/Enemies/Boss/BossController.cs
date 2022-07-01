using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {
    protected int _healthPoints = 700;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private int _portalBefore;
    [SerializeField] private int _portalAfter;
    [SerializeField] private int _isle;

    #region Animator
    private Animator _animator;
    public Animator Animator {get { return _animator; } set { _animator = value;}}
    #endregion

    private BossStateMachine _stateMachine;
    
    #region State Machine
    public BossStateMachine StateMachine;
    public BossFactory StateFactory;

    public GameObject Projectile;
    #endregion

    private void Start() {
        _healthPoints = 500;
        _animator = GetComponent<Animator>();

        // State Machine
        StateMachine = new BossStateMachine(this);
        StateFactory = new BossFactory(this, StateMachine);
        StateMachine.Initialize(StateFactory.BossWalk);
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
        StateMachine.ChangeState(StateFactory.BossDeath);

        Events.OnActivatePortal.Invoke(_portalBefore);
        Events.OnActivatePortal.Invoke(_portalAfter);
        Events.OnCleanZone.Invoke(_isle);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
    #endregion

    public GameObject InstantiateObj(Vector3 second, Quaternion third) {
        return Instantiate(Projectile, second, third);
    }
}
