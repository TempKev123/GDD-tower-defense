using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
   // Each wave is an array of (row, type) tuples
private (int row, int type)[][] waves = new (int, int)[][]
{//new (int, int)[] { (0,0), (1,1), (2,1), (3,2), (4,2) }
    new (int, int)[] { (3,0) }
    ,new (int, int)[] { (3,1), }/*
    ,new (int, int)[] { (0,0), (1,1), (2,1), }
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}
    ,new (int, int)[]{}*/
};


    private int maxwaves; 
    public static int activeEnemies= 0;
    
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public float spawnInterval = 10f; // How often to spawn
                                    //300 seconds for 5 min | 15 waves
    public GameObject winScreen; 
    private float timer;
    private int waveNumber = 1;

    void Start()
    {
        maxwaves=waves.Length;
        SpawnWave(0);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && waveNumber < maxwaves)
        {
            SpawnWave(waveNumber);
            waveNumber++;
            timer = 0f;
        }
        else
        {
            winGame();
        }
    }

    void SpawnEnemy(int row,int type)
    {
        Vector3 spawnPos = transform.position;

        GameObject prefabToSpawn = enemyPrefabs[type];
    
        int[] lanes = { -4, -2, 0, 2, 4 };//blocks/lanes are 2 apart

        Instantiate(
            prefabToSpawn,
            spawnPos + new Vector3(lanes[row], 0, 0),
            Quaternion.Euler(0, 180, 0)
        );


    }

   void SpawnWave(int waveNum)
{
    if (waveNum >= maxwaves ) 
    {
        Debug.Log("All waves completed!");
        //winScreen.SetActive(true);
        return ; // No more waves to spawn
    }
    var currWave = waves[waveNum]; // this is (int enemyType, int count)[]
    for (int i = 0; i < currWave.Length; i++)
    {
        var (row, type) = currWave[i];
            SpawnEnemy(row,type); // pass enemyType to your spawn logic
    }
}
    void winGame()
    {
        if (activeEnemies <= 0 && waveNumber >= maxwaves)
        {
            winScreen.SetActive(true);
        }
    }
}

