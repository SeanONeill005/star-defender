using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    int totalScore = 0;

    public GameObject enemy1Prefab;
    public GameObject enemy3Prefab;
    private float spawnRange = 9;
    private int waveChoice;
    public int enemyCount;
    public int waveNumber = 1;
    private int wave1Rows = 3;
    private int wave1Columns = 6;

    public GameObject powerupPrefab;
    private float powerupTimer = 0.0f;
    private float nextPowerupTime = 10.0f;
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

        powerupTimer += Time.deltaTime;
        if (powerupTimer > nextPowerupTime)
        {
            powerupTimer = 0.0f;
            nextPowerupTime = Random.Range(10.0f, 20.0f);
            Vector3 position = new Vector3(Random.Range(-spawnRange, spawnRange), 11, 0);
            Instantiate(powerupPrefab, position, powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
       
        if (waveChoice == 1)
        {
            for (int row = 0; row < wave1Rows; row++)
            {
                for (int column = 0; column < wave1Columns; column++)
                {
                    Vector3 position = new Vector3(-3.75f + column * 1.5f, 9.5f - row * 1.0f, 0);
                    Instantiate(enemy1Prefab, position, enemy1Prefab.transform.rotation);
                }
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
