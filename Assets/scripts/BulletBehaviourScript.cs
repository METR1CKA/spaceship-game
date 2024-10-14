using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBehaviourScript : MonoBehaviour
{
    Vector2 bullet_velocity;

    // Start is called before the first frame update
    void Start()
    {
        float x = PlayerPrefs.GetFloat("x", 0f);
        float y = PlayerPrefs.GetFloat("y", +.2f);
        bullet_velocity.x = x;
        bullet_velocity.y = y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().position += bullet_velocity;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
