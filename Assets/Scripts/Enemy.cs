using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
[SerializeField] GameObject deathFX;
[SerializeField] GameObject enemyHit;
//[SerializeField] Transform parent;
[SerializeField] int enemyHealth = 50;
[SerializeField] int enemyScorePerHit = 10;

ScoreBoard scoreBoard;
GameObject parentObject;  
WeaponDamage weaponDamage;

void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>(); 
        parentObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); //add rigid body to the game object this script is on (declaring a variable for clarity)
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
{
    ProcessHit(other);
    if (enemyHealth < 1){ KillEnemy();}
}
void ProcessHit(GameObject weapon)
{
    ProcessScore();
    GameObject fx = Instantiate(enemyHit, transform.position, Quaternion.identity); //current position, no rotation
    fx.transform.parent = parentObject.transform;
    weaponDamage = weapon.GetComponent<WeaponDamage>();
    enemyHealth -= weaponDamage.getWeaponDamage();
    //Debug.Log($"{this.name} has {enemyHealth} life left.");
}
void KillEnemy()
{
    GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity); //current position, no rotation
    vfx.transform.parent = parentObject.transform;
    vfx.GetComponent<AudioSource>().Play();
    Destroy(this.gameObject);
}
void ProcessScore()
{
    scoreBoard.IncreaseScore(enemyScorePerHit);
}
}
