using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
[SerializeField] GameObject enemyExplosion;
[SerializeField] GameObject enemyHit;
[SerializeField] Transform parent;
[SerializeField] int enemyHealth = 50;
[SerializeField] int enemyScorePerHit = 10;

ScoreBoard scoreBoard;
WeaponDamage weaponDamage;

void Start() 
{
   scoreBoard = FindObjectOfType<ScoreBoard>(); 
}

void OnParticleCollision(GameObject other)
{
    ProcessScore();
    ProcessHit(other);
    if (enemyHealth <= 0){ KillEnemy();}
}

void ProcessScore()
{
    scoreBoard.IncreaseScore(enemyScorePerHit);
}

void ProcessHit(GameObject weapon)
{
    GameObject vfx = Instantiate(enemyHit, transform.position, Quaternion.identity); //current position, no rotation
    vfx.transform.parent = parent;
    weaponDamage = weapon.GetComponent<WeaponDamage>();
    enemyHealth -= weaponDamage.getWeaponDamage();
    //Debug.Log($"{this.name} has {enemyHealth} life left.");
}
void KillEnemy()
{
    GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity); //current position, no rotation
    vfx.transform.parent = parent;
    Destroy(this.gameObject);
}
}
