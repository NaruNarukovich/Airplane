using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            collision.gameObject.GetComponent<Player>().isHaveTimerBonus = true;
            collision.gameObject.GetComponent<Player>().StartCoroutine("TimerBonusTimer");
            Destroy(gameObject);
        }
    }
}
