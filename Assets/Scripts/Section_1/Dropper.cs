using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float timeToWait = 3f;
    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }
    private void Update() {
        if(Time.time > timeToWait){
            rigidBody.useGravity = true;
        }
    }
}
