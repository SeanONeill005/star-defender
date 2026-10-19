using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy3Prefab;
    public TextMeshProUGUI score;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    private int waveChoice;
    int totalScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveChoice = 1;
        score.text = "Score: " + totalScore;
        SpawnEnemyWave(waveNumber);
    }

    public void AddScore(int points)
    {
        totalScore += points;
        score.text = "Score: " + totalScore;
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 0);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveChoice == 1)
        {
            enemyCount = FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
        }
        else if (waveChoice == 2)
        {
            enemyCount = FindObjectsByType<Enemy3Controller>(FindObjectsSortMode.None).Length;
        }

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
       
        if (waveChoice == 1)
        {
            for (int i = 0; i < enemiesToSpawn / 2; i++)
            {
                Instantiate(enemy1Prefab, GenerateSpawnPosition(), enemy1Prefab.transform.rotation);
            }
            waveChoice = 2;
        }
        if (waveChoice == 2)
        {
            for (int i = 0; i < enemiesToSpawn / 2; i++)
            {
                Instantiate(enemy3Prefab, GenerateSpawnPosition(), enemy3Prefab.transform.rotation);
            }
            waveChoice = 1;
        }



    }

}
