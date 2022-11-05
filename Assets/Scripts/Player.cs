using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speedX;
    public float speedY;

    public float speedYmodify;

    public float minX;
    public float maxX;

    public GameObject bulletPrefab;

    public GameObject bulletRegular;
    public GameObject bulletDoubleBonus;

    public int bullets;
    public Text bulletsText;

    public float timer;
    public float timerMax;

    public float timerMaxRegular;
    public float timerMaxBonus;

    public bool isHaveDoubleBullet;
    public bool isHaveTimerBonus;
    public bool isHaveSecondLife;
    public bool isHaveTripleBullet;
    public bool isHaveHomingMissile;

    private SpriteRenderer spriteRenderer;
    public Sprite spriteRegular;
    public Sprite spriteSecondLifeBonus;

    public Sprite[] spritesRegular;
    public Sprite[] spritesBonus;

    public bool isDev;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int currentSkin = PlayerPrefs.GetInt("CurrentSkin");

        spriteRegular = spritesRegular[currentSkin];
        spriteSecondLifeBonus = spritesBonus[currentSkin];
    }

    private void FixedUpdate()
    {
        speedY += speedYmodify;

        Vector3 position = transform.position;

        if (!isDev) 
        {
            position.y += speedY;
        }
        else 
        {
            position.y += speedY * 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speedX;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speedX;
        }

        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }

    public void Death()
    {
        if (!isDev) 
        {
            if (isHaveSecondLife)
            {
                isHaveSecondLife = false;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) 
        {
            isDev = !isDev;
        }

        if (isHaveDoubleBullet) 
        {
            bulletPrefab = bulletDoubleBonus;
        }
        else 
        {
            bulletPrefab = bulletRegular;
        }

        if (isHaveTimerBonus) 
        {
            timerMax = timerMaxBonus;
        }
        else 
        {
            timerMax = timerMaxRegular;
        }

        if (isHaveSecondLife) 
        {
            spriteRenderer.sprite = spriteSecondLifeBonus;
        }
        else 
        {
            spriteRenderer.sprite = spriteRegular;
        }

        timer--;

        if (Input.GetKey(KeyCode.Space) && bullets > 0 && timer <= 0)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y++;

            timer = timerMax;

            if (!isDev)
                bullets--;

            if (!isHaveTripleBullet) 
            {
                GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                newBullet.GetComponent<Bullet>().isHaveHomingMissile = isHaveHomingMissile;
            }
            else 
            {
                //Quaternion rotationOne = new Quaternion(0, 0, -45, 0);
                //Quaternion rotationSecond = new Quaternion(0, 0, 45, 0);

                GameObject firstBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                GameObject secondBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);
                Instantiate(bulletPrefab, spawnPosition, Quaternion.identity, null);

                firstBullet.transform.Rotate(0, 0, 25);
                secondBullet.transform.Rotate(0, 0, -25);

                firstBullet.GetComponent<Bullet>().direction = 1;
                secondBullet.GetComponent<Bullet>().direction = 2;
            }
            
        }

        bulletsText.text = bullets.ToString();
    }

    IEnumerator DoubleBulletTimer()
    {
        yield return new WaitForSeconds(10);
        isHaveDoubleBullet = false;
    }

    IEnumerator TimerBonusTimer()
    {
        yield return new WaitForSeconds(10);
        isHaveTimerBonus = false;
    }

    IEnumerator TripleBulletTimer() 
    {
        yield return new WaitForSeconds(10);
        isHaveTripleBullet = false;
    }
}
