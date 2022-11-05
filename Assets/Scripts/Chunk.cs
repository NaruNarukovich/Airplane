using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y += 40;

            Instantiate(gameManager.chunks[Random.Range(0, gameManager.chunks.Length)], spawnPosition, Quaternion.identity, null);

            Destroy(gameObject);
        }
    }
}
