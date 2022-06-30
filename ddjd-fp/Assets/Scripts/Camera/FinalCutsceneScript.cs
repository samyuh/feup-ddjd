using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Timeline;

public class FinalCutsceneScript : MonoBehaviour
{
    public PlayableDirector timeline;
    public TimelineAsset finalTimelineAsset;
    public GameObject platform;  
    public CinemachineBrain brain;

    private bool play = false;  

    // Start is called before the first frame update
    void Start() {
        brain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();

        var trackList = finalTimelineAsset.GetOutputTracks();
        foreach (var track in trackList) {
            if (track.name == "Cinemachine Track") {
                timeline.SetGenericBinding(track, brain);
            }
        }
    }

    void Update() {
        if (play) {
            play = false;
            timeline.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")  {
            play = true;
        }
    }
}
