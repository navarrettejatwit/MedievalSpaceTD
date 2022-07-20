using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy EnemyPrefab;

    [SerializeField] private Enemy EnemyPrefab1;

    [SerializeField] private GameObject enemies = null;

    [SerializeField] private int EnemiesAtStart = 0;

    [SerializeField] private int wave = 1; 

    [SerializeField] private float timeBetween = 0;

    [SerializeField] private float spawnTime = 0;

    [SerializeField] private float timeBeforeNextWave = 60;

    private int EnemyPerWave = 0;

    private EnemyFactory[] EnemyFactories = new EnemyFactory[2];

    private EnemyFactory enemyFactory;

    private Transform[] SpawnPoints = null;

    private Transform SpawnPoint;

    private bool needSpawnPoint = false;

    private int i = 0;

    private int j = 0;

    private Enemy e;

    void Start()
    {
        EnemyPerWave = EnemiesAtStart;
        EnemyFactories[0] = new EnemyFactory(EnemyPrefab, enemies);
        EnemyFactories[1] = new EnemyFactory(EnemyPrefab1, enemies);
        SpawnPoints = GameObject.FindGameObjectWithTag("EnemySpawnPoints").GetComponentsInChildren<Transform>();
        needSpawnPoint = true;
    }
    
    void Update()
    {
        spawnTime -= Time.deltaTime;
        spawnWave();
    }
    
    public void spawnWave()
    {
        if (spawnTime <= 0f)
        {
            for (int i = 0; i < EnemyPerWave; i++)
            {
                getSpawnPoint();
                getEnemy(SpawnPoint);
            }
            spawnTime = timeBetween;
        }
    }

    public void getSpawnPoint()
    {
        if (needSpawnPoint)
        {
            i = Random.Range(1, SpawnPoints.Length);
        }
        SpawnPoint = SpawnPoints[i];
    }

    public void getEnemy(Transform SpawnPoint)
    {
        int j = Random.Range(1, EnemyFactories.Length);
        EnemyFactories[j].setSpawnPoint(SpawnPoint);
        e = (Enemy) EnemyFactories[j].produce();                        
        e.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 90));
    }

}
