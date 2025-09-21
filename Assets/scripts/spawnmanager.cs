using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
   // Each wave is an array of (row, type) tuples 
   //WHAT AM I DOING? THIS DOESNT SEEM RIGHT
private (int row, int type)[][][] waves = new (int, int)[][][]
{
    // --- Wave Set 1 ---
    new (int, int)[][]
    {
        new (int, int)[] { (2,0) },
        new (int, int)[] { (2,1) },
        new (int, int)[] { (0,0), (1,1), (2,1) },
        new (int, int)[] { (0,0),(3,0),(2,2) },
        new (int, int)[] { (2,3),(0,0),(4,0) },
        new (int, int)[] { (0,0),(3,2),(4,2) },
        new (int, int)[] { (4,1),(0,2),(2,2) },
        new (int, int)[] { (0,0),(1,0),(2,3), },
        new (int, int)[] { (0,0),(1,0),(2,3),(3,2),(4,1) },
        new (int, int)[] { (0,1),(1,1),(2,1),(4,1) },
        new (int, int)[] { (3,2),(4,1) },
        new (int, int)[] { (0,0),(1,3),(2,2),(3,3),(4,1) },
        new (int, int)[] { (1,3),(2,3),(3,3) },
        new (int, int)[] { (0,0),(1,1),(2,2),(3,1),(4,1) },
        new (int, int)[] { (0,3),(2,4),(4,3) }
    },

    // --- Wave Set 2 ---
    new (int, int)[][]
    {
        new (int, int)[] { (0,0) },
        new (int, int)[] { (3,1), (4,1) },
        new (int, int)[] { (0,0), (3,0), (4,0) },
        new (int, int)[] { (0,1), (1,1), (3,1), (4,2) },
        new (int, int)[] { (0,3), (1,3), (2,2) },
        new (int, int)[] { (0,3), (1,3), (3,3), },
        new (int, int)[] { (0,2), (2,3),(4,1) },
        new (int, int)[] { (0,2), (1,2), (2,3), (3,3), },
        new (int, int)[] { (0,2), (1,0), (2,2), (3,0), (4,0) },
        new (int, int)[] { (0,0), (1,0), (2,0), (3,2), (4,0) },
        new (int, int)[] { (0,0), (1,0), (2,0), (3,0), (4,0) },
        new (int, int)[] { (0,1), (1,1), (2,2), (3,1), (4,0) },
        new (int, int)[] { (0,0), (1,0), (2,3), (3,0), (4,0) },
        new (int, int)[] { (2,4)},
        new (int, int)[] { (0,0), (1,3), (3,3), (4,0) },

    },

    // --- Wave Set 3 ---
    new (int, int)[][]
    {
        new (int, int)[] { (2,1),(4,0) },
        new (int, int)[] { (1,0), (2,1), (4,1) },
        new (int, int)[] { (0,3), (1,0), (2,3), (3,3), (4,0) },
        new (int, int)[] { (0,0), (1,2), (4,1) },
        new (int, int)[] { (0,0), (1,0), (2,0), (3,1), (4,1) },
        new (int, int)[] { (2,4)},
        new (int, int)[] { (2,3), (3,3), (4,0) },
        new (int, int)[] { (0,2), (1,2), (2,2), (3,2), (4,2) },
        new (int, int)[] { (0,1), (1,3), (2,3), },
        new (int, int)[] { (0,1), (1,1), (2,0), (4,0) },
        new (int, int)[] { (0,1), (1,0), (2,3),(4,0) },
        new (int, int)[] { (0,1), (1,3), (2,2), (4,1) },
        new (int, int)[] { (3,4) },
        new (int, int)[] { (0,0) },
        new (int, int)[] { (1,4) }

    },
    new (int, int)[][]
    {

        new (int, int)[] { (2,4)},
        new (int, int)[] { (0,0), (1,3), (3,3), (4,0) },
        new (int, int)[] { (3,4) },
        new (int, int)[] { (0,0) },
        new (int, int)[] { (1,4) }
    }
};
    private int maxwaves; 
    public static int activeEnemies= 0;
    public int chooselevel=0;
    
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public float spawnInterval = 20f; // How often to spawn
                                    //300 seconds for 5 min | 15 waves
    public GameObject winScreen; 
    private float timer;
    private int waveNumber = 1;

    void Start()
    {
        maxwaves=waves[chooselevel].Length;
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
    if (waveNum > maxwaves ) 
    {
        Debug.Log("All waves completed!");
        //winScreen.SetActive(true);
        return ; // No more waves to spawn
    }
    var currWave = waves[chooselevel][waveNum]; // this is (int enemyType, int count)[]
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

