using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyOneBehaviourScript : MonoBehaviour
{
    Vector2 bullet_velocity;
    SoundBehaviourScript soundScript;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        soundScript = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundBehaviourScript>();
    }

    public void SetDirection(Vector2 direction)
    {
        bullet_velocity = direction * 0.1f;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += bullet_velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spaceship" || collision.gameObject.tag == "bullet")
        {
            soundScript.PlayAudioEnemy(1);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
