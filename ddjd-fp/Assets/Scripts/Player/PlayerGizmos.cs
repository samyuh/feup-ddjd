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
        Gizmos.color = Color.blue;
        Vector3 spherePosition2 = new Vector3(transform.position.x + 0.616f * transform.TransformDirection(Vector3.forward).x, transform.position.y + 0.3f, 
                                        transform.position.z + 0.616f * transform.TransformDirection(Vector3.forward).z);

        Gizmos.DrawSphere(spherePosition2, 0.5f);

        // Close Up Attack Sphere
        Gizmos.color = Color.green;
        Vector3 spherePosition3 = new Vector3(transform.position.x + 2f * transform.TransformDirection(Vector3.forward).x, transform.position.y + 0.3f, 
                                        transform.position.z + 2f * transform.TransformDirection(Vector3.forward).z);

        Gizmos.DrawSphere(spherePosition3, 1.3f);
    }
}
