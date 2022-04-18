using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGizmos : MonoBehaviour{
    private void OnDrawGizmos() {
        // Grounded Sphere
        Gizmos.color = Color.red;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - 0.58f, transform.position.z);
        Gizmos.DrawSphere(spherePosition, 0.35f);

        // Attack Sphere
    }
}
