using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().isHaveTripleBullet = true;
            collision.gameObject.GetComponent<Player>().isHaveDoubleBullet = false;
            collision.gameObject.GetComponent<Player>().StartCoroutine("TripleBulletTimer");
            Destroy(gameObject);
        }
    }
}
