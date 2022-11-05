using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public float timer;
    public float timerMax;
    public Boss script;
    public GameObject[] enemyPrefab;
    public float offset;

    private void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= offset;
            spawnPosition.x = Random.Range(-4.2f, 4.2f);
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPosition, Quaternion.identity, null);

            timer = timerMax;
        }
    }
}

        
       
