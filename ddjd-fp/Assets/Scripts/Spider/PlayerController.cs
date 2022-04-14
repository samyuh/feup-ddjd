using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player movement script


    private float MoveSpeed { get; } = 3.8f;
    private float RotSpeed { get; } = 80.0f;

    private GameObject player;

   

    void Update()
    {
        // Handle keyboard control
        // This loop competes with AdjustBodyTransform() in LegController script to properly postion the body transform

        // float ws = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
        transform.Translate(0, 0, 0.01f);

        // float ad = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        transform.Translate(0.01f, 0, 0);

        // if (Input.GetKey(KeyCode.Q))
        // {
        //     transform.Rotate(0, -RotSpeed * Time.deltaTime, 0);
        // }
        // if (Input.GetKey(KeyCode.E))
        // {
        //     transform.Rotate(0, RotSpeed * Time.deltaTime, 0);
        // }
    }
}
