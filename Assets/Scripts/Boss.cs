using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //1 - летимо до низу, 2 - ліворуч та праворуч якщо > 50% hp, 3 - < 50% hp
    public int stage;
    public int bossIndex;

    public float distance;
    private float newDistance;

    public float distanceMin;
    public float distanceMax;

    private float positionX;

    public Transform player;

    public float speedY;
    public float speedX;

    public float minX;
    public float maxX;

    public bool isRight;

    public float health;
    private float healthMax;

    public EnemySpawner enemySpawner;
    public EnemyShooting enemyShooting;

    private Vector3 targetPosition;

    public float minY;
    public float maxY;

    private float normalHeight;

    private void Start()
    {
        normalHeight = transform.position.y;
        targetPosition = transform.position;
        healthMax = health;

        enemyShooting = GetComponent<EnemyShooting>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine("NewDistance");
    }

    private void FixedUpdate()
    {
        speedY = player.GetComponent<Player>().speedY;

        if (stage == 1)
        {
            if (distance >= Vector3.Distance(transform.position, player.position))
            {
                stage = 2;
            }
        }

        if (stage == 2 || stage == 3) 
        {
            positionX = transform.position.x;
            targetPosition = player.position;

            if (isRight)
            {
                positionX += speedX;

                if (positionX > maxX)
                {
                    isRight = false;
                }
            }
            else
            {
                positionX -= speedX;

                if (positionX < minX)
                {
                    isRight = true;
                }
            }

            distance = Mathf.MoveTowards(distance, newDistance, speedY);

            targetPosition.y += distance;
            targetPosition.x = positionX;

            transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
        }
    }

    IEnumerator NewDistance() 
    {
        yield return new WaitForSeconds(2);
        newDistance = Random.Range(distanceMin, distanceMax);
        StartCoroutine("NewDistance");
    }

    public void Damage(float damage) 
    {
        health -= damage;

        if (health <= healthMax / 2 && stage == 2) 
        {
            stage = 3;
            enemyShooting.timerMax /= 2;

            speedX *= 2;

            enemyShooting.countOfEnemy = 2;
        }

        if (health <= 0)
        {
            float timeOfBattle = player.position.y - enemySpawner.distances[enemySpawner.indexBoss];

            enemySpawner.indexBoss++;
            enemySpawner.StartCoroutine("AfterBoss");

            enemySpawner.distances[enemySpawner.indexBoss] += (int)timeOfBattle;

            for (int i = 0; i < Random.Range(7, 15); i++)
            {
                Vector3 spawnPosition = transform.position;
                spawnPosition.x += Random.Range(-3f, 3f);
                spawnPosition.y += Random.Range(-3f, 3f);

                Instantiate(enemySpawner.bonuses[Random.Range(0, enemySpawner.bonuses.Length)], spawnPosition, Quaternion.identity, null);
            }

            Destroy(gameObject);
        }
    }
}
