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
        _context.PlayerInput.PlayerAim.canceled -= OnAimCancelled;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        base.PlayerRotation();
        
        companion.transform.position = companionPlace.transform.position;
        companion.transform.rotation = Quaternion.RotateTowards(companion.transform.rotation, companionPlace.transform.rotation, 100f * Time.deltaTime);
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    private void OnThrow(InputAction.CallbackContext contextInput) {
        Vector2 screenCenterPoint = new Vector2(Screen.width /2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (readyToThrow && _context.ActiveCrystal != null) {
           if ((_context.Data.ManaCrystal >= 100) && (Physics.Raycast(ray, out RaycastHit hit))) {
                GameObject projectile;
                Rigidbody projectileRb;
                Vector3 direction;

                GameObject.FindGameObjectWithTag("CompanionAnimator").GetComponent<Animator>().SetTrigger(_context.ActiveCrystal.name);
                switch(_context.ActiveCrystal.name) {
                    case "Obsidia":
                        projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile, companion.transform.position, _context.Camera.MainCamera.transform.rotation);

                        _context.Data.ManaCrystal -= 100;
                        Events.OnCrystalManaUpdate.Invoke(_context.Data.ManaCrystal, _context.Data.MaxMana);
                        break;
                    case "Air":
                        projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile, companion.transform.position, _context.Camera.MainCamera.transform.rotation);
                        projectileRb = projectile.GetComponent<Rigidbody>();
                        direction = (hit.point - companion.transform.position);
                        projectileRb.AddForce(direction.normalized *  throwForce, ForceMode.Impulse);
                        projectileRb.angularDrag = 100;

                        _context.Data.ManaCrystal -= 100;
                        Events.OnCrystalManaUpdate.Invoke(_context.Data.ManaCrystal, _context.Data.MaxMana);
                        break;
                    case "Fire":
                        projectile = _context.InstantiateObj(_context.ActiveCrystal.crystalProjectile, companion.transform.position, _context.Camera.MainCamera.transform.rotation);
                        projectileRb = projectile.GetComponent<Rigidbody>();
                        direction = (hit.point - companion.transform.position);
                        projectileRb.AddForce(direction.normalized *  throwForce, ForceMode.Impulse);
                        projectileRb.angularDrag = 100;

                        _context.Data.ManaCrystal -= 100;
                        Events.OnCrystalManaUpdate.Invoke(_context.Data.ManaCrystal, _context.Data.MaxMana);
                        break;
                }
            }
        }
    }
    

    protected virtual void OnAimCancelled(InputAction.CallbackContext context) {
        _stateMachine.ChangeState(_factory.IdleState);
    }

}