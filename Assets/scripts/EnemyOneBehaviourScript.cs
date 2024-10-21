using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneBehaviourScript : MonoBehaviour
{
    Vector2 enemy_velocity;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public Sprite enemyDestroyed1;
    public Sprite enemyDestroyed2;
    float next_shot_time;
    SoundBehaviourScript soundScript;

    int hit;

    // Start is called before the first frame update
    void Start()
    {
        soundScript = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundBehaviourScript>();
        hit = 0;
        next_shot_time = Time.time + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > next_shot_time)
        {
            Shoot();
            next_shot_time += 1.0f;
        }
    }

    public void SetVelocityAndRotation(Vector2 velocity)
    {
        enemy_velocity = velocity;

        if (velocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<BulletEnemyOneBehaviourScript>().SetDirection(enemy_velocity.normalized);
    }


    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += enemy_velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "bullet" || tag == "spaceship")
        {
            soundScript.PlayAudioEnemy(1);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (tag == "bullet")
        {
            hit++;

            if (hit == 1)
            {
                GetComponent<SpriteRenderer>().sprite = enemyDestroyed1;
            }

            if (hit == 2)
            {
                GetComponent<SpriteRenderer>().sprite = enemyDestroyed2;
            }

            if (hit == 3)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                int score = PlayerPrefs.GetInt("score");
                score += 20;
                PlayerPrefs.SetInt("score", score);
            }
        }

        if (tag == "spaceship")
        {
            Destroy(gameObject);
        }
    }
}
