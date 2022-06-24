using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject Puzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckPressed()
    {
        Vector3 spherePosition = new Vector3(transform.position.x + 0.616f * transform.TransformDirection(Vector3.forward).x, transform.position.y - 0.1f, 
                                        transform.position.z + 0.616f * transform.TransformDirection(Vector3.forward).z);

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, 0.2f);
        foreach (var hitCollider in hitColliders) {
            
            if (hitCollider.gameObject.tag == "Player") {
                Puzzle.SendMessage("StartReseting");
            }
        }
    }
}
