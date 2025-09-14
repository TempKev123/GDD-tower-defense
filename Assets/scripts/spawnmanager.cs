using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public GameObject bossPrefab; // Boss prefab
    public float spawnInterval = 0.7f; // How often to spawn

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {//blocks/lanes are 2 apart
        // Pick random spawn point within an area
        Vector3 spawnPos = transform.position;

        // Pick a random prefab from the list
        int whospawns=Random.Range(0, 10);
        int index = 0;
        if (whospawns<4){
            index=0;
        }
        else if (whospawns<8){
            index=1;
        }
        else{
            index=2;
        }
        GameObject prefabToSpawn = enemyPrefabs[index];
    
        int[] lanes = { -4, -2, 0, 2, 4 };
        int randomLane = lanes[Random.Range(0, lanes.Length)];

        Instantiate(
            prefabToSpawn,
            spawnPos + new Vector3(randomLane, 0, 0),
            Quaternion.Euler(0, 180, 0)
        );


    }
    public void SpawnBoss()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(bossPrefab, spawnPos, Quaternion.Euler(0, 180, 0));
    }
}

