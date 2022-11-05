using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public float speed;
    public bool isPlayer;

    public GameObject bullet;
    public bool isDouble;

    public int direction;

    public bool isHaveHomingMissile;

    private Transform currentEnemy;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) 
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                currentEnemy = GameObject.FindGameObjectsWithTag("Enemy")[i].transform;

                if (Vector3.Distance(transform.position, GameObject.FindGameObjectsWithTag("Enemy")[i].transform.position) < Vector3.Distance(transform.position, currentEnemy.transform.position))
                {
                    currentEnemy = GameObject.FindGameObjectsWithTag("Enemy")[i].transform;
                }
            }
        }
        else 
        {
            isHaveHomingMissile = false;
        }


        Destroy(gameObject, 1.5f);
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;

        if (isPlayer) 
        {
            if (player.isDev) 
            {
                speed = player.speedY * 10;
            }
            else 
            {
                speed = player.speedY * 5;
            }
        }

        if (!isHaveHomingMissile) 
        {
            if (isPlayer == true) 
            {
                position.y += speed;

                if (direction == 1) 
                {
                    position.x -= speed / 2;
                }
                else if (direction == 2)
                {
                    position.x += speed / 2;
                }
            }
            else 
            {
                position.y -= speed;
            }
        }
        else 
        {
            // get the angle
            Vector3 norTar = (currentEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;

            // rotate to angle
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle - 90);
            transform.rotation = rotation;

            GetComponent<Rigidbody2D>().AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() && isPlayer == true) 
        {
            collision.gameObject.GetComponent<Enemy>().Death();

        }

        if (collision.gameObject.GetComponent<Boss>() && isPlayer == true)
        {
            collision.gameObject.GetComponent<Boss>().Damage(damage);

        }

        if (collision.gameObject.GetComponent<Player>() && isPlayer == false)
        {
            collision.gameObject.GetComponent<Player>().Death();
        }

        if (isDouble)
        {
            Instantiate(bullet, transform.position, Quaternion.identity, null);
        }

        Destroy(gameObject);
    }
}
