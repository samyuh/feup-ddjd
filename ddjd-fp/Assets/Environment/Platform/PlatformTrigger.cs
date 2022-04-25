using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("MoveUp");
        if(other.CompareTag("Player"))
        {
            myAnimationController.SetBool("UpBool", true);
            Debug.Log("true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("MoveDown");
        if(other.CompareTag("Player"))
        {
            myAnimationController.SetBool("UpBool", false);
            Debug.Log("false");
        }
    }
}
