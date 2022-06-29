using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MasterGameObject : MonoBehaviour {
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}