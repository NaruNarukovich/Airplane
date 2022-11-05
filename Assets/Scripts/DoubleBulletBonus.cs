using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBulletBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()) 
        {
            if (collision.gameObject.GetComponent<Player>().isHaveTripleBullet == false) 
            {
                collision.gameObject.GetComponent<Player>().isHaveDoubleBullet = true;
            }
            collision.gameObject.GetComponent<Player>().StartCoroutine("DoubleBulletTimer");
            Destroy(gameObject);
        }
    }
}
