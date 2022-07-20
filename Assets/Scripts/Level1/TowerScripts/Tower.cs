using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, Product
{

    [SerializeField] private GameObject Projectiles = null;
    [SerializeField] private GameObject arrow = null;
        
    [SerializeField] private float rateOfFire = 0;

    [SerializeField] private float fireTime = 0;
    
    [SerializeField] private int damage = 0;
    
    [SerializeField] private int health = 0;
    private int cost;

    private int layermask;

    private Ray ray;

    private Enemy e = null;

    void Awake(){
        layermask = LayerMask.GetMask("Enemies");
        ray = new Ray(this.transform.position, new Vector3(10,0,0));
    }
  
    void Update()
    {
        if (e != null)
        {
            this.takeDamage(e.doDamage());
        }

        RaycastHit hit;
        Debug.DrawRay(this.transform.position, new Vector3(10,0,0), Color.green);
        if (Physics.Raycast(ray, out hit, 10f, layermask))
        {
            fireTime -= Time.deltaTime;
            if(fireTime <= 0f){
                arrow = Instantiate(arrow, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, Projectiles.transform);
                arrow.transform.rotation = Quaternion.Euler(0f, 90f,0f);
                fireTime = rateOfFire;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            e = collision.gameObject.GetComponent<Enemy>();
        }
    }


    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            e.setIsMoving();
            Destroy(this.gameObject);
        }
    }

    private int doDamage()
    {
        return this.damage;
    }
}


