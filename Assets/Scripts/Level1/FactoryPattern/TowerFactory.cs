using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : Factory
{
    private Tower t;
    
    private Transform SpawnPoint;

    private GameObject towers;

    public TowerFactory(Tower t, GameObject towers)
    {
        this.t = t;
        this.towers = towers;
    }

    public Product produce()
    {
        return spawn(t, SpawnPoint, towers);
    }

    private Product spawn(Tower t, Transform tsp, GameObject towers)
    {
        return Object.Instantiate(t, new Vector3(tsp.position.x, tsp.position.y, tsp.position.z-1), Quaternion.identity, towers.transform);
    }

    public void setSpawnPoint(Transform tsp)
    {
        SpawnPoint = tsp;
    }
}
