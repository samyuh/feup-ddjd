using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreenScript : MonoBehaviour
{
    public GameObject startingScreen;
    public GameObject menuScreen;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            startingScreen.SetActive(false);
            menuScreen.SetActive(true);
        }
    }
}
