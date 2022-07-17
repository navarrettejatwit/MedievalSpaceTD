using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : Factory
{
    private Enemy e;

    private Transform SpawnPoint;

    private GameObject enemies;

    public EnemyFactory(Enemy e, GameObject enemies)
    {
        this.e = e;
        this.enemies = enemies;
    }

    public Product produce()
    {
        return spawn(e, SpawnPoint, enemies);
    }

    private Product spawn(Enemy e, Transform esp, GameObject enemies)
    {
        return Object.Instantiate(e, new Vector3(esp.position.x, esp.position.y, esp.position.z), Quaternion.identity, enemies.transform);
    }

    public void setSpawnPoint(Transform esp)
    {
        SpawnPoint = esp;
    }
}

