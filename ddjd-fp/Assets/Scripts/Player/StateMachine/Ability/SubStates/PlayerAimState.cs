using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerAbilityState {
    [Header("Throwing")]
    private float throwCooldown = 1f;
    private float throwForce = 15f;
    private float throwUpwardForce = 1f;
    private bool readyToThrow = true; 
    private GameObject companion = GameObject.Find("Companion");

    public PlayerAimState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        readyToThrow = true;

        Events.OnToggleAim.Invoke();
        _context.PlayerInput.PlayerMeleeAttack.performed += OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;
        
    }  

    public override void ExitState() {
        base.ExitState();

        Events.OnToggleAim.Invoke();
        _context.PlayerInput.PlayerMeleeAttack.performed -= OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        // after x elapsed time ready to throw  = true

        // Some movement of the player, 

        // change player angle PlayerRotation on base
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    private void OnThrow(InputAction.CallbackContext contextInput) {
        // Condition for Regular shooting
        if (readyToThrow && _context.ActiveCrystal != null) {
            // ABILITIES

            // DEFAULT (Obsidia Rocks from the ground)
            // To Do

            // Air (Tornado)
            // GameObject projectile = _context.InstantiateObj(_context.SecondaryObjectToThrow, companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
            // projectile.transform.LookAt(_context.Camera.MainCamera.transform.position +  _context.Camera.MainCamera.transform.forward * 100f);    

            // Fire (Fire Ball)
            GameObject projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile,  companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            Vector3 forceToAdd = _context.Camera.MainCamera.transform.forward * throwForce + _context.transform.up * throwUpwardForce;
            projectileRb.AddForce(forceToAdd,ForceMode.Impulse);

            // Earth (To be defined)  
        } else if (readyToThrow) {
            // TORNADO
            GameObject projectile = _context.InstantiateObj(_context.SecondaryObjectToThrow, companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
            projectile.transform.LookAt(_context.Camera.MainCamera.transform.position +  _context.Camera.MainCamera.transform.forward * 100f);    
        }

    }

    protected virtual void OnAimCancelled(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.IdleState);
    }

}