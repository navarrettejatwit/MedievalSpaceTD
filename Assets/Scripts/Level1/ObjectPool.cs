using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS SCRIPT IS JUST A REFERENCE
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    //THIS METHOD SHOULD BE IN AN OBJECT THAT NEEDS TO GET FROM POOL
    //GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(); 
        //if (bullet != null) {
        //bullet.transform.position = turret.transform.position;
        //bullet.transform.rotation = turret.transform.rotation;
        //bullet.SetActive(true);
    //}
    
    //THIS SET TO DEACTIVATE AN OBJECT INSTEAD OF DESTROY.
    //gameobject.SetActive(false);

}
