using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;

    private void OnTriggerMoveUp(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimationController.SetBool("UpBool", true);
        }
    }

    private void OnTriggerMoveDown(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimationController.SetBool("UpBool", false);
        }
    }
}
