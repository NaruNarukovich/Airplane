using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemySpawner>();
        Destroy(gameObject, 20f);
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;

        position.y -= speed;

        transform.position = position;
    }

    public void Death() 
    {
        if (Random.Range(0, 10) == 0)
        {
            Instantiate(enemySpawner.bonuses[Random.Range(0, enemySpawner.bonuses.Length)],
                transform.position, Quaternion.identity, null);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()) 
        {
            collision.gameObject.GetComponent<Player>().Death();
            Death();
        }

        Destroy(gameObject);
    }
}
