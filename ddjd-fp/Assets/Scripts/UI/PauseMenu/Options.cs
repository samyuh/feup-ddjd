using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] public bool grass;
    [SerializeField] public bool mageisle;
    
    public GameObject grassSelected;
    public GameObject mageIsleSelected;

    void Update()
    {
        grassSelected.SetActive(grass);
        mageIsleSelected.SetActive(mageisle);     
    }    

    public void ToggleGrass()
    {
        grass = !grass;
        Debug.Log(grass);
    }

    public void ToggleMageIsle()
    {
        mageisle = !mageisle;
        Debug.Log(mageisle);
    }
}
