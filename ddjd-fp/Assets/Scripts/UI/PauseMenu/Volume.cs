using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider mySlider;
    FMOD.Studio.Bus Master;

    void Start()
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        Master.setVolume(mySlider.value);
    }

    void Update()
    {
        Master.setVolume(mySlider.value);
    }
}
