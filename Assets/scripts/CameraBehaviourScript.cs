using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviourScript : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject powerUpPrefab;
    public GameObject powerUp2Prefab;
    public GameObject powerUp3Prefab;
    float next_spawn_time;
    float next_enemy1_spawn_time;
    float next_enemy2_spawn_time;
    bool spawnEnemy1 = false;
    bool spawnEnemy2 = false;
    float screenWidth = 7.5f;
    float screenHeight = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        next_spawn_time = Time.time + 4.0f;
        next_enemy1_spawn_time = Time.time + 4.0f;
        next_enemy2_spawn_time = Time.time + 5.0f;
        spawnEnemy1 = false;
        spawnEnemy2 = false;

        PlayerPrefs.SetInt("score", 0);
        if (!PlayerPrefs.HasKey("max_score"))
        {
            PlayerPrefs.SetInt("max_score", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > next_spawn_time)
        {
            SpawnAsteroids();
            next_spawn_time += 2.0f;
        }

        int score = PlayerPrefs.GetInt("score");

        if (score >= 100 && score < 300)
        {
            if (!spawnEnemy1)
            {
                spawnEnemy1 = true;
                next_enemy1_spawn_time = Time.time + 4.0f;
            }

            if (spawnEnemy1 && Time.time > next_enemy1_spawn_time)
            {
                SpawnEnemy1();
                next_enemy1_spawn_time += 4.0f;
            }
        }

        if (score >= 300 && score < 600)
        {
            if (spawnEnemy1)
            {
                spawnEnemy1 = false;
            }

            if (!spawnEnemy2)
            {
                spawnEnemy2 = true;
                next_enemy2_spawn_time = Time.time + 4.0f;
            }

            if (spawnEnemy2 && Time.time > next_enemy2_spawn_time)
            {
                SpawnEnemy2();
                next_enemy2_spawn_time += 4.0f;
            }
        }

        if (score >= 600)
        {
            if (!spawnEnemy1)
            {
                spawnEnemy1 = true;
                next_enemy1_spawn_time = Time.time + 4.0f;
            }

            if (spawnEnemy2)
            {
                next_enemy2_spawn_time = Time.time + 5.0f;
            }

            if (spawnEnemy1 && Time.time > next_enemy1_spawn_time)
            {
                SpawnEnemy1();
                next_enemy1_spawn_time += 4.0f;
            }

            if (spawnEnemy2 && Time.time > next_enemy2_spawn_time)
            {
                SpawnEnemy2();
                next_enemy2_spawn_time += 5.0f;
            }
        }
    }

    public (Vector2 position, Vector2 direction) spawner()
    {
        int edge = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;
        Vector2 direction = Vector2.zero;

        if (edge == 0)
        {
            spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight);
            direction = new Vector2(0, -1);
        }
        else if (edge == 1)
        {
            spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight);
            direction = new Vector2(0, 1);
        }
        else if (edge == 2)
        {
            spawnPosition = new Vector2(-screenWidth, Random.Range(-screenHeight, screenHeight));
            direction = new Vector2(1, 0);
        }
        else if (edge == 3)
        {
            spawnPosition = new Vector2(screenWidth, Random.Range(-screenHeight, screenHeight));
            direction = new Vector2(-1, 0);
        }

        return (spawnPosition, direction);
    }

    public void SpawnAsteroids()
    {
        var (position, direction) = spawner();
        GameObject newAsteroid = Instantiate(asteroid, position, Quaternion.identity);
        newAsteroid.GetComponent<AsteroidBehaviourScript>().SetVelocity(direction * 0.03f);
    }

    public void SpawnEnemy1()
    {
        var (position, direction) = spawner();
        GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        newEnemy.GetComponent<EnemyOneBehaviourScript>().SetVelocityAndRotation(direction * 0.04f);
    }

    public void SpawnEnemy2()
    {
        var (position, direction) = spawner();
        GameObject newEnemy = Instantiate(enemyPrefab2, position, Quaternion.identity);
        newEnemy.GetComponent<EnemyOneBehaviourScript>().SetVelocityAndRotation(direction * 0.04f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
