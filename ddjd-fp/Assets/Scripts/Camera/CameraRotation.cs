
using UnityEngine;

public class CameraRotation : MonoBehaviour {
   public GameObject _mainCamera;

   void Start() {
      _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
   }
     
   void Update () {
      transform.rotation = _mainCamera.transform.rotation;
   }
}