using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipBehaviourScript : MonoBehaviour
{
    Vector2 ship_velocity;
    public GameObject bulletPrefab;
    public GameObject boton;
    public GameObject corazon1, corazon2, corazon3, corazon4, corazon5;
    public GameObject lastChange;
    public SoundBehaviourScript soundBehaviourScript;

    int hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ship_velocity.x = -.15f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ship_velocity.x = +.15f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            ship_velocity.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ship_velocity.y = +.15f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ship_velocity.y = -.15f;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            ship_velocity.y = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            soundBehaviourScript.PlayAudioBala(0);

            float angle = Mathf.Round(transform.rotation.eulerAngles.z);

            if (angle >= 0 && angle <= 90)
            {
                float dir = angle * 100 / 90;
                float x = dir * 1 / 100;
                float y = 1 - x;
                PlayerPrefs.SetFloat("x", -x);
                PlayerPrefs.SetFloat("y", y);
            }

            if (angle > 90 && angle <= 180)
            {
                float dir = ((angle - 90) * 100) / 90;
                float x = -(1 - (dir * 1) / 100);
                float y = -(dir * 1) / 100;
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
            }

            if (angle > 180 && angle <= 270)
            {
                float dir = ((angle - 180) * 100) / 90;
                float x = (dir * 1) / 100;
                float y = -(1 - x);
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
            }

            if (angle > 270 && angle <= 360)
            {
                float dir = ((angle - 270) * 100) / 90;
                float x = 1 - (dir * 1) / 100;
                float y = (dir * 1) / 100;
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
            }

            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += ship_velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        string[] tags = { "enemy1", "enemy2", "asteroid", "enemy_bullet1", "enemy_bullet2" };

        if (tags.Contains(tag))
        {
            hit++;

            if (hit == 1)
            {
                corazon1.SetActive(false);
            }

            if (hit == 2)
            {
                corazon2.SetActive(false);
            }

            if (hit == 3)
            {
                corazon3.SetActive(false);
            }

            if (hit == 4)
            {
                corazon4.SetActive(false);
            }

            if (hit == 5)
            {
                corazon5.SetActive(false);
                lastChange.SetActive(true);
            }

            if (hit == 6)
            {
                soundBehaviourScript.PlayAudioNave(2);
                soundBehaviourScript.StopBackgroundMusic();
                Destroy(gameObject);
                Destroy(collision.gameObject);
                boton.SetActive(true);
                int score = PlayerPrefs.GetInt("score");
                if (PlayerPrefs.HasKey("max_score"))
                {
                    int max_score = PlayerPrefs.GetInt("max_score");
                    if (score > max_score)
                    {
                        PlayerPrefs.SetInt("max_score", score);
                    }
                }
            }
        }
    }
}
