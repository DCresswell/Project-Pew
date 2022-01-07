using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
[SerializeField] GameObject enemyExplosion;
[SerializeField] Transform parent;
[SerializeField] int enemyPoints = 10;

ScoreBoard scoreBoard;

void Start() 
{
   scoreBoard = FindObjectOfType<ScoreBoard>(); 
}

private void OnParticleCollision(GameObject other) 
{
    Debug.Log("hit!!!");  
    GameObject vfx = Instantiate(enemyExplosion, transform.position, Quaternion.identity); //current position, no rotation
    vfx.transform.parent = parent;
    Destroy(this.gameObject);  
    scoreBoard.IncreaseScore(enemyPoints);
}

}
