using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] Slider lifeSlider;
    
    public event Action OnDestroy;

    GameObject bullet;
    
    float timer = .3f;
    float angle = 0;
    int movePattern = -1;
    int bulletPattern = 0;

    int movePatternCount = 3;
    int bulletPatternCount = 2;

    int targetX = 0;
    int targetY = 2;
    Vector3 target;
    int flip = 0;

    int life = 100;

    bool isStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0, 10, 0);
        target = new Vector3(0, targetY, 0);
        explosionParticle.Stop();
        lifeSlider.value = 100;
        lifeSlider.GetComponent<CanvasGroup>().alpha = 0;
     }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            switch (bulletPattern)
            {
                case 0:
                    break;
                case 1:
                    bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().dir = Quaternion.Euler(0, 0, angle) * new Vector3(0, -1, 0); //new Vector3(0, -1, 0);
                    bullet.GetComponent<Bullet>().speed = 30f;
                    bullet.GetComponent<Bullet>().tag = "Enemy";

                    timer = .03f;
                    angle += 30;

                    break;
                case 2:
                    for (int i = 0; i < 12; i++)
                    {
                        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                        bullet.GetComponent<Bullet>().dir = Quaternion.Euler(0, 0, angle + 30 * i) * new Vector3(0, -1, 0); //new Vector3(0, -1, 0);
                        bullet.GetComponent<Bullet>().speed = 30f;
                        bullet.GetComponent<Bullet>().tag = "Enemy";
                    }
                    timer = .3f;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        switch (movePattern)
        {
            case -1:
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2);
                break;
            case 0:
                break;
            case 1:
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2);
                break;
            case 2:
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2);
                break;
        }
        
        if (Vector3.Distance(pos, target) < 0.05f)
        {
            if (!isStart)
            {
                StartCoroutine(LifeSliderFadeIn());
                isStart = true;
            }

            flip += 1; // こんな変な計算を
            flip %= 2; // しなくてもええんやで

            movePattern = flip + 1;
            bulletPattern = flip + 1;

            targetX = 6 * (flip == 0 ? 1 : -1);
            target = new Vector3(targetX, targetY, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet" && collision.tag == "Enemy") return;

        //gameManager.GetComponent<BulletHellManager>().Hoge();

        if (collision.name.StartsWith("Bullet") && collision.tag == "Player")
        {
            life -= 10;
            lifeSlider.value -= 10;

            if (life <= 0)
            {
                explosionParticle.transform.position = this.transform.position;
                explosionParticle.Play();

                OnDestroy.Invoke();

                Destroy(gameObject);
            }
        }
    }

    IEnumerator LifeSliderFadeIn()
    {
        float timer = 0f;
        float duration = 3f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            lifeSlider.GetComponent<CanvasGroup>().alpha = Mathf.Clamp01(timer / duration);
            yield return null;
        }
        lifeSlider.GetComponent<CanvasGroup>().alpha = 1f;
    }
}
