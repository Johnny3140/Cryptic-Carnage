using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 10f;  // Time between each wave/round
    private int currentRound = 1;

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            int numberOfZombies = currentRound * 2;  // Adjust this formula as needed
            SpawnWave(numberOfZombies);
            yield return new WaitForSeconds(timeBetweenWaves);
            currentRound++;
        }
    }

    void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    public void ZombieDefeated()
{
}
}
