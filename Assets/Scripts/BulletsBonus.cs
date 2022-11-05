using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBonus : MonoBehaviour
{
    public int count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>()) 
        {
            collision.gameObject.GetComponent<Player>().bullets += count;
            Destroy(gameObject);
        }
    }
}
