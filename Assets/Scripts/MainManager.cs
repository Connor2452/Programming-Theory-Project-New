using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private float spawnRangeX = 70.0f;
    [SerializeField] private float spawnRangeY = 45.0f;

    [SerializeField] private GameObject[] Enemies;
    private int enemyIndex;

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;

    private int enemyCount;
    private int waveNumber = 1;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("player");
        AreEnemiesLeft(); 
        if (!player)
        {
            GameOver();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, -1);

        return spawnPos;
    }

    private void AreEnemiesLeft()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemyIndex = Random.Range(0, Enemies.Length);
            Instantiate(Enemies[enemyIndex], GenerateSpawnPosition(), Enemies[enemyIndex].transform.rotation);
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
