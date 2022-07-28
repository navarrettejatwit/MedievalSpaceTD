using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, Product
{

    [SerializeField] private GameObject Projectiles = null;

	private int ammo = 8;

	[SerializeField] private GameObject Projectile = null;

	private GameObject currentProjectile;

    [SerializeField] private float rateOfFire = 0;

    [SerializeField] private float fireTime = 0;
    
    [SerializeField] private int health = 0;

	private int originalHealth;

	private float originalFireTime;
	
	private float originalRateOfFire;

    private int layermask;

    private Ray ray;

    private GameObject a;

    public int cost;

	private GameObject munition;

    void Awake(){
		GameObject temp;
		originalHealth = health;
		originalFireTime = fireTime;
		originalRateOfFire = rateOfFire;
        layermask = LayerMask.GetMask("Enemies");
        ray = new Ray(this.transform.position, new Vector3(10,0,0));
		for(int i=0;i<ammo;i++){
			temp = Instantiate(Projectile, Projectiles.transform);
			temp.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
			temp.SetActive(false);
		}
    }
  
    void Update()
    {
	    RaycastHit hit;
        Debug.DrawRay(this.transform.position, new Vector3(10,0,0), Color.green);
        if (Physics.Raycast(ray, out hit, 10f, layermask))
        {
            fireTime -= Time.deltaTime;
            if(fireTime <= 0f){
				currentProjectile = getProjectile();
				if (currentProjectile != null) {
        			currentProjectile.transform.position = this.transform.position;
        			currentProjectile.transform.rotation = Quaternion.Euler(0f, 90f,0f);
        			currentProjectile.SetActive(true);
    			}	
                fireTime = rateOfFire;
            }
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
	        //this.gameObject.SetActive(false)
	        Destroy(this.gameObject);
			//return true;
        }
		//return false;
    }

	private GameObject getProjectile()
	{
		for(int i = 0; i<ammo; i++)
        {
            if(!this.gameObject.transform.GetChild(1).GetChild(i).gameObject.activeInHierarchy)
            {
                return this.gameObject.transform.GetChild(1).GetChild(i).gameObject;
            }
        }
        return null;
	}

	public void resetTower(){
		health = originalHealth;
		rateOfFire = originalRateOfFire;
		fireTime = originalFireTime;
	}
}


