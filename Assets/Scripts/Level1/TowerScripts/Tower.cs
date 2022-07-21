using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, Product
{

    [SerializeField] private GameObject Projectiles = null;

	[SerializeField] private GameObject Projectile = null;

    [SerializeField] private float rateOfFire = 0;

    [SerializeField] private float fireTime = 0;
    
    [SerializeField] private int health = 0;

    private int layermask;

    private Ray ray;

    private GameObject a;

    private Enemy e = null;
    public int cost;

	private GameObject munition;

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
                Instantiate(Projectile, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f, 90f,0f), Projectiles.transform);
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

}


