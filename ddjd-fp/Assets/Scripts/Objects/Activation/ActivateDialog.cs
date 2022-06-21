using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialog : MonoBehaviour {
    [SerializeField] private DialogManager _currentDialog;

    void OnTriggerStay(Collider collider) {
        if (collider.tag == "Player") {
            Events.OnDialog.Invoke(_currentDialog);
            Destroy(gameObject);
        }
    }
}
