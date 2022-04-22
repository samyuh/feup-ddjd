using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingTutorial : MonoBehaviour
{

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow; 

    // Start is called before the first frame update
    private void Start(){
        readyToThrow = true;
    }

    // Update is called once per frame
    private void Update(){
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0){
            Throw();
        }
    }

    private void Throw(){
        readyToThrow = false;

        // Instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow,attackPoint.position,cam.rotation);

        // Get RigidBody Component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Add Force
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd,ForceMode.Impulse);

        totalThrows--;

        // Implement Throw Cooldown
        Invoke(nameof(ResetThrow),throwCooldown);

    }

    private void ResetThrow(){

        readyToThrow = true;
    }
}
