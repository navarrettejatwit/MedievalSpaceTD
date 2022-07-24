using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy EnemyPrefab;

    [SerializeField] private Enemy EnemyPrefab1;

    [SerializeField] private GameObject enemies = null;

    [SerializeField] private int MaxEnemies = 0;
    
    [SerializeField] private int EnemyPerWave;

    [SerializeField] public int wave = 0; 

    [SerializeField] private float timeBetween = 0;

    [SerializeField] private float spawnTime = 0;

    [SerializeField] private float timeBeforeNextWave = 60;
    
    public TextMeshProUGUI enemyCount;
    
    public TextMeshProUGUI waveCountUI;
    
    public TextMeshProUGUI waveCD;

    private EnemyFactory[] EnemyFactories = new EnemyFactory[2];

    private EnemyFactory enemyFactory;

    private Transform[] SpawnPoints = null;

    private Transform SpawnPoint;

    private bool needSpawnPoint = false;

    private int i = 0;

    private Enemy e;

    private GameObject temp;

    void Start()
    {
        EnemyFactories[0] = new EnemyFactory(EnemyPrefab, enemies);
        EnemyFactories[1] = new EnemyFactory(EnemyPrefab1, enemies);
        SpawnPoints = GameObject.FindGameObjectWithTag("EnemySpawnPoints").GetComponentsInChildren<Transform>();
        needSpawnPoint = true;
        for(int k=0;k<MaxEnemies;k++){
            int j = Random.Range(1,EnemyFactories.Length);
            getSpawnPoint();
            EnemyFactories[j].setSpawnPoint(SpawnPoint);
            e = (Enemy) EnemyFactories[j].produce();
            e.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 90));
            e.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        spawnTime -= Time.deltaTime;
        spawnWave();
        if (EnemyPerWave != enemies.transform.childCount)
        {
            int x = EnemyPerWave;
            enemyCount.text = "Enemies: " + (EnemyPerWave - (x - enemies.transform.childCount));
        }
        waveCD.text = "Next wave in: " + Mathf.Round(timeBeforeNextWave);
        if (EnemyPerWave == 0)
        {
            wave++;
            waveCountUI.text = "Wave: " + wave;
        }
    }

    public void spawnWave()
    {
        if (spawnTime <= 0f && EnemyPerWave != 0)
        {
            getSpawnPoint();
            temp = getEnemy();
            if (temp != null)
            {
                temp.transform.position = new Vector3(SpawnPoint.position.x,SpawnPoint.position.y,SpawnPoint.position.z);
                temp.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 90));
				temp.GetComponent<Enemy>().resetEnemy();
                temp.gameObject.SetActive(true);
            }
            spawnTime = timeBetween;
        }
        else
        {
            timeBeforeNextWave -= Time.deltaTime;
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

    public GameObject getEnemy()
    {
        for(int k = 0; k<MaxEnemies; k++)
        {
            if(!enemies.gameObject.transform.GetChild(k).gameObject.activeInHierarchy)
            {
                return enemies.gameObject.transform.GetChild(k).gameObject;
            }
        }
        return null;
    }

}
