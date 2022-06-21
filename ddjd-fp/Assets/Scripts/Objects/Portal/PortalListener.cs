using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalListener : MonoBehaviour {
    [SerializeField] private int id;
    [SerializeField] private int _numberButton;
    [SerializeField] private GameObject teleportPassage;

    public void Awake() {
        Events.OnActivatePortal.AddListener(ActivatePortal);
    }

    private void ActivatePortal(int portalNumber) {
        if (portalNumber == id) {
            _numberButton -= 1;

            if (_numberButton == 0) {
                Debug.Log("Activate");
                teleportPassage.SetActive(true);
            }
        }
    }
}
