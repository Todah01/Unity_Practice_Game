using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource collision_as;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start() {
        collision_as = this.GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    private void OnCollisionEnter(Collision other) {

        if(isTransitioning || collisionDisabled)
            return;

        switch(other.gameObject.tag){
            case "Friendly":
                
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
     }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        collision_as.Stop();
        collision_as.PlayOneShot(success);
        successParticles.Play();
        this.GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence(){
        isTransitioning = true;
        collision_as.Stop();
        collision_as.PlayOneShot(crash);
        crashParticles.Play();
        this.GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
     }

     void ReloadLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
     }
     void LoadNextLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx + 1;
        if(nextSceneIdx >= SceneManager.sceneCountInBuildSettings)
            nextSceneIdx = 0;
        SceneManager.LoadScene(nextSceneIdx);
     }
}