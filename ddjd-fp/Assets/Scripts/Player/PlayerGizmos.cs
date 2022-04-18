using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGizmos : MonoBehaviour{
    [SerializeField] private PlayerSettings _playerSettings;

    private void OnDrawGizmos() {

        // Grounded Sphere
        Gizmos.color = Color.red;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - _playerSettings.GroundedOffset, transform.position.z);
        Gizmos.DrawSphere(spherePosition, _playerSettings.GroundedRadius);

        // Attack Sphere
    }
}
