using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCamera : MonoBehaviour
{
    public GameObject _mainCamera;

   void Start() {
      _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
   }
     
   void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _mainCamera.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = _mainCamera.transform.rotation;

   }
}
