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
    private GameObject companion = GameObject.FindGameObjectWithTag("Companion");
    private GameObject playerAimTarget = GameObject.FindGameObjectWithTag("PlayerAimTarget");
    private GameObject companionPlace = GameObject.Find("CompanionPlace");
    private int layerMask;


    public PlayerAimState(Player currentContext, StateMachine playerStateFactory, StateFactory stateFactory) : 
    base (currentContext, playerStateFactory, stateFactory) { }

    public override void EnterState() {
        base.EnterState();
        readyToThrow = true;

        Events.OnToggleAim.Invoke();
        _context.PlayerInput.PlayerMeleeAttack.performed += OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;

        layerMask = 1 << LayerMask.NameToLayer("Companion");
        layerMask = ~layerMask;
        
    }  

    public override void ExitState() {
        base.ExitState();

        Events.OnToggleAim.Invoke();
        _context.PlayerInput.PlayerMeleeAttack.performed -= OnThrow;
        _context.PlayerInput.PlayerAim.canceled += OnAimCancelled;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        base.PlayerRotation();
        // after x elapsed time ready to throw  = true

        // Some movement of the player, 

        // change player angle PlayerRotation on base

        // Keep companion looking at target while aiming
        companion.transform.position = companionPlace.transform.position;
        companion.transform.rotation = Quaternion.RotateTowards(companion.transform.rotation, companionPlace.transform.rotation, 100f * Time.deltaTime);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    private void OnThrow(InputAction.CallbackContext contextInput) {
        Vector2 screenCenterPoint = new Vector2(Screen.width /2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        // Condition for Regular shooting
        if (readyToThrow && _context.ActiveCrystal != null) {
            // ABILITIES
            GameObject projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile, companion.transform.position, _context.Camera.MainCamera.transform.rotation);

            /*
            switch(_context.ActiveCrystal.name) {
                case "Obsidia":
                    
                    toShot = true;
                    break;
                case "Air":
                    projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile,  companion.transform.position, _context.Camera.MainCamera.transform.rotation);
                    toShot = true;
                    break;

                case "Earth":

                    projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile,  companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
                    
                    Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                    Vector3 forceToAdd = _context.Camera.MainCamera.transform.forward * throwForce + _context.transform.up * throwUpwardForce;
                    projectileRb.AddForce(forceToAdd,ForceMode.Impulse);                    
   
                    break;

                case "Fire":
                    // GameObject projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile,  companion.transform.position + new Vector3(0f, 0f, 0f), _context.Camera.MainCamera.transform.rotation);
                    // Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                    // Vector3 forceToAdd = _context.Camera.MainCamera.transform.forward * throwForce + _context.transform.up * throwUpwardForce;
                    // projectileRb.AddForce(forceToAdd,ForceMode.Impulse);
                    break;
            }
            */
            

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                Vector3 direction = (hit.point - companion.transform.position);
                projectileRb.AddForce(direction.normalized *  throwForce, ForceMode.Impulse);
            }
        }
    }

    protected virtual void OnAimCancelled(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.IdleState);
    }

}