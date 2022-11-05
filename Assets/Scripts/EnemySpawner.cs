using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    public GameObject[] enemys;
    public GameObject[] bonuses;

    public int[] distances;

    private bool isSpawned;

    public int bonusChanse;

    public int indexBoss;
    public GameObject[] bosses;
    public bool isFightWithBoss;

    private void Update()
    {
        if (isFightWithBoss) 
        {

        }
        else 
        {
            if ((int)player.position.y == distances[indexBoss]) 
            {
                isFightWithBoss = true;

                Vector3 spawnPosition = new Vector3(0, player.position.y + 30, 0);

                GameObject currentBoss = Instantiate(bosses[indexBoss], spawnPosition, Quaternion.identity, null);
                currentBoss.GetComponent<Boss>().enemySpawner = this;
            }

            if ((int)player.position.y % 10 == 0 && !isFightWithBoss)
            {
                if (isSpawned == false)
                {
                    int random = Random.Range(1, 5);

                    for (int i = 0; i < random; i++)
                    {
                        Vector3 spawnPosition = player.position;
                        spawnPosition.y += Random.Range(20f, 25f);
                        spawnPosition.x = Random.Range(-4.2f, 4.2f);

                        if (Random.Range(0, bonusChanse) == 0)
                        {
                            Instantiate(bonuses[Random.Range(0, bonuses.Length)], spawnPosition, Quaternion.identity, null);
                        }
                        else
                        {
                            GameObject enemy = gameObject;

                            if (player.position.y < 100)
                            {
                                enemy = Instantiate(enemys[0], spawnPosition, Quaternion.identity, null);

                            }
                            else
                            {
                                enemy = Instantiate(enemys[Random.Range(0, enemys.Length)], spawnPosition, Quaternion.identity, null);
                            }

                            enemy.GetComponent<Enemy>().enemySpawner = this;
                        }
                    }

                    isSpawned = true;
                }
            }
            else
            {
                isSpawned = false;
            }
        }
    }

    IEnumerator AfterBoss()
    {
        yield return new WaitForSeconds(5);
        isFightWithBoss = false;
    }
}
