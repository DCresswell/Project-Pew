using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem blast;
    void OnCollisionEnter(Collision other) 
    {  
        Debug.Log(gameObject.name + " bumped into " + other.gameObject.name); 
    }

    void OnTriggerEnter(Collider other) 
    {
        // string interpolation syntax
        //Debug.Log($"{gameObject.name} triggered by {other.gameObject.name}"); 
        CrashSequence();
    }

    void CrashSequence()
    {
        GetComponent<PlayerControls>().enabled=false;
        blast.Play();  //play explosion
        GetComponent<MeshRenderer>().enabled=false;  
        GetComponent<BoxCollider>().enabled=false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
    }
}
