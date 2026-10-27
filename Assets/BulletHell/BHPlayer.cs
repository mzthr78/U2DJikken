using System;
using Unity.VisualScripting;

//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class BHPlayer : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] Image[] lifeImage;

    public event Action OnDestroy;

    float moveH = 0;
    float moveV = 0;

    float speed = .2f;

    float screenHalfHeight = 0f;
    float screenHalfWidth = 0f;

    int life = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHalfHeight * Camera.main.aspect;

        explosionParticle.Stop();
    }

    float timer = .5f;

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().dir = new Vector3(0, 1, 0);
            bullet.GetComponent<Bullet>().speed = 20f;
            bullet.GetComponent<Bullet>().tag = "Player";
        }
    }

    void FixedUpdate()
    {

        transform.position = transform.position + new Vector3(moveH, moveV, 0) * speed;

        // 画面外に出られないように
        // ちゃんとやるなら自機の大きさの分を足したり引いたり
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -screenHalfWidth, screenHalfWidth),
            Mathf.Clamp(transform.position.y, -screenHalfHeight, screenHalfHeight),
            0
        );
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Bullet") && collision.tag == "Enemy") {

            life -= 1;

            lifeImage[life].enabled = false;

            if (life <= 0)
            {
                explosionParticle.transform.position = this.transform.position;
                explosionParticle.Play();

                OnDestroy.Invoke();

                Destroy(gameObject);
            }
        }
    }
}
 