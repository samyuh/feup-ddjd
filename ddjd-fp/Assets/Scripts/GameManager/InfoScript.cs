using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScript : MonoBehaviour
{
    public static Dictionary<string, Vector3> savedPositions = new Dictionary<string, Vector3>(){
        {"Isle 1", new Vector3(0f, 0f, 0f)},
        {"Isle 2", new Vector3(0f, 0f, 0f)},
        {"Isle 3", new Vector3(0f, 0f, 0f)},
        {"Isle 4", new Vector3(0f, 0f, 0f)},
        {"Isle 5", new Vector3(0f, 0f, 0f)},
        {"Isle 6", new Vector3(0f, 0f, 0f)},
    };

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
}
