using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public float timeBetweenSpawns = 2f;
    public int zombiesPerRoundIncrease = 2;

    private int zombiesToSpawn;

    void Start()
    {
        GameManager.OnRoundStart += OnRoundStart; 
    }

    void OnRoundStart()
    {
        zombiesToSpawn = GameManager.instance.currentRound * zombiesPerRoundIncrease;
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        for (int i = 0; i < zombiesToSpawn; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnZombie()
    {
        Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
