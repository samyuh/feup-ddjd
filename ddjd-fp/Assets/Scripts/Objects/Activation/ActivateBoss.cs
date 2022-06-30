using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour {
    [SerializeField] private int _portal;
    [SerializeField] private BossController _bossController;

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            _bossController.enabled = true;
            Events.OnDeactivatePortal.Invoke(_portal);
            // ANIMATION
            Destroy(gameObject);
        }
    }
}
