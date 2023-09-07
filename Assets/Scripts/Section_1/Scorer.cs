using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] int score = 0;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Obstacle_Default"){
            score++;
            Debug.Log(score);
        }
    }
}
