using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour
{
    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            Events.OnDialog.Invoke();
            Destroy(gameObject);
        }
    }
}
