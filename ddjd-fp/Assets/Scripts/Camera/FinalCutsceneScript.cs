using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalCutsceneScript : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject platform;  

    private bool play = false;  

    // Start is called before the first frame update
    void Start()
    {
        timeline.GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if(play)
        {
            play = false;
            timeline.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            play = true;
        }
    }
}
