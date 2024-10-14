using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidBehaviourScript : MonoBehaviour
{
    Vector2 asteroid_velocity;

    public Sprite asteroid1;
    public Sprite asteroid2;
    public GameObject explosion;

    SoundBehaviourScript soundScript;

    int hit;

    // Start is called before the first frame update
    void Start()
    {
        soundScript = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundBehaviourScript>();

        float scale = Random.Range(+.8f, +.9f);
        Vector2 asteroid_scale = new Vector2(scale, scale);

        transform.localScale = asteroid_scale;

        hit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += asteroid_velocity;
    }

    public void SetVelocity(Vector2 velocity)
    {
        asteroid_velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "bullet" || tag == "spaceship")
        {
            soundScript.PlayAudioAsteroid(1);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        if (tag == "bullet")
        {
            hit++;

            if (hit == 1)
            {
                GetComponent<SpriteRenderer>().sprite = asteroid1;
            }

            if (hit == 2)
            {
                GetComponent<SpriteRenderer>().sprite = asteroid2;
            }

            if (hit == 3)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                int score = PlayerPrefs.GetInt("score");
                score += 10;
                PlayerPrefs.SetInt("score", score);
            }
        }

        if (tag == "spaceship")
        {
            Destroy(gameObject);
        }
    }
}
