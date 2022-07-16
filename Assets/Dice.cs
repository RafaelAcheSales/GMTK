using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    Vector3 initialPosition;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }
    void Update()
    {
        rb.AddForce(Vector3.forward * 10);
        //resets position sets random rotation and random velocity
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.position = initialPosition;
            transform.rotation = Random.rotation;
        }
    }
}
