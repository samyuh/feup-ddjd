using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Float : MonoBehaviour {

    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float floatinPower = 15f;

    public float waterHeight = 0f;

    private Rigidbody m_Rigidbody;
    private GameObject player;

    bool underwater;

    private void Start(){
        m_Rigidbody = GetComponent<Rigidbody>();
        player =  GameObject.Find("PlayerCameraTarget");

    }

    private void FixedUpdate(){
        float difference = transform.position.y - waterHeight - player.transform.position.y ;

        if(difference < 0){
            m_Rigidbody.AddForceAtPosition(Vector3.up * floatinPower * Mathf.Abs(difference),transform.position,ForceMode.Force);

            if(!underwater){
                underwater = true;
                SwitchState(true);
            }

        }else if(underwater){
            underwater = false;
            SwitchState(false);
        }
    }


    private void SwitchState(bool isUnderwater){

        if(isUnderwater){
            m_Rigidbody.drag = underwaterDrag;
            m_Rigidbody.angularDrag = underwaterAngularDrag;
        }else{
            m_Rigidbody.drag = airDrag;
            m_Rigidbody.angularDrag = airAngularDrag;
        }
    }

}