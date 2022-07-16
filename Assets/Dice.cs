using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    List<Transform> diceSides = new List<Transform>();
    public Transform player;
    public Vector3 offset;
    public float torqueSpeed;
    public float gravity;

    public float resultSpeedLimit;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        foreach (Transform child in transform) {
            diceSides.Add(child);
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * gravity);
        //resets position sets random rotation and random velocity
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            resetDice();
        }
        if (rb.velocity.magnitude < resultSpeedLimit && rb.angularVelocity.magnitude < resultSpeedLimit) {
            getResult();
            
        }
    }
    void getResult() {
        int result = 0;
        float bestValue = 0;
        foreach (Transform child in diceSides) {
            if (child.position.z < bestValue) {
                bestValue = child.position.z;
                result = int.Parse(child.name);
            }
        }
        print("Result: " + result);
    }
    void resetDice() {
        rb.MovePosition(player.transform.position + offset);
        transform.rotation = Random.rotation;
        rb.AddRelativeTorque(Random.insideUnitSphere * torqueSpeed);
    }
}
