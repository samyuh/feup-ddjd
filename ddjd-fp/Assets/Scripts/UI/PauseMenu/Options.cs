using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] public bool grass;
    
    public GameObject grassSelected;

    void Update()
    {
        grassSelected.SetActive(grass);  
    }    

    public void ToggleGrass()
    {
        grass = !grass;
        Debug.Log(grass);
    }
}
