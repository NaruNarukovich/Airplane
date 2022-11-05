using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float timer;
    public float timerMax;

    public GameObject bulletPrefab;
    public GameObject enemyPrefab;

    public float offset;

    public bool boosCanSpawnEnemy;
    public int countOfEnemy;

    private void FixedUpdate()
    {
        timer--;

        if (timer <= 0) 
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y -= offset;

            if (boosCanSpawnEnemy) 
            {
                for (int i = 0; i < countOfEnemy; i++) 
                {
                    spawnPosition.x = Random.Range(-4.2f, 4.2f);
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, null);
                }
            }
            else 
            {
                Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
            }

            timer = timerMax;
        }
    }
}
