using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public bool isPressed = false;

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetKey("e"))
        {
            isPressed = true;
            Debug.Log("Button was pressed!");
        }
    }
}
