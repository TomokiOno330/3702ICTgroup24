using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject enemies;
    [SerializeField] float appearTime;
    [SerializeField] int maxEnemies;
    private int enemyNumber;
    private float waitInterval;

    private void Start()
    {
        enemyNumber = 0;
        waitInterval = 0f;
    }

    void Update(){
        if (enemyNumber >= maxEnemies){
            return;
        }
        waitInterval = waitInterval + Time.deltaTime;
        if (waitInterval > appearTime){
            waitInterval = 0f;

            SpawnEnemy();
        }
    }
    void SpawnEnemy(){
        var randomRotationY = Random.value * 360f;
        
        GameObject.Instantiate(enemies, transform.position, Quaternion.Euler(0f, randomRotationY, 0f));
        enemyNumber++;
        waitInterval = 0f;
    }

  
}