using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;
    List<Transform> diceSides = new List<Transform>();
    public KeyCode throwKey;
    bool rolling = false;
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
        resetDice();
    }
    private void Update() {
        if (Input.GetKeyDown(throwKey)) {
            resetDice();
        }
        if (HasStopped() && rolling) {
            getResult();
            rolling = false;
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * gravity);
        //resets position sets random rotation and random velocity

    }
    bool HasStopped() {
        return rb.velocity.magnitude < resultSpeedLimit &&
         rb.angularVelocity.magnitude < resultSpeedLimit;
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
        SkillsManager.Instance.ChangeSkill(KeyCodeToIndex(throwKey), result-1);
    }
    void resetDice() {
        rb.MovePosition(player.transform.position + offset);
        transform.rotation = Random.rotation;
        rb.AddRelativeTorque(Random.insideUnitSphere * torqueSpeed);
        StartCoroutine(resetDiceRolling());
    }
    IEnumerator resetDiceRolling() {
        yield return new WaitForSeconds(0.6f);
        rolling = true;
    }

    int KeyCodeToIndex(KeyCode key) {
        switch (key) {
            case KeyCode.Alpha1:
                return 0;
            case KeyCode.Alpha2:
                return 1;
            case KeyCode.Alpha3:
                return 2;
            default:
                return -1;
        }
    }

}
