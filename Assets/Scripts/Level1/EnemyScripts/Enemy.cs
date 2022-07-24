using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Product
{
    [SerializeField] private float speed;
    
    [SerializeField] private int damage;

    [SerializeField] private int health;

	private int originalHealth;

    private GameObject player;

    private Player p;

    private Projectile projectile;

    private Enemy e;

    private Tower tower;

    public int reward;

    private bool hasGivenCash = false;

    private bool isMoving = true;

    void Start()
    {
		originalHealth = health;
        Physics.IgnoreLayerCollision(1,5);
		Physics.IgnoreLayerCollision(7,7);
    }
    
    void Update()
    {
        if (tower != null)
        {
            isMoving = tower.takeDamage(damage);
        }
        moving();
    }
    void Awake()
    {
        player = GameObject.Find("Player");
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
                case "Player":
                    p = collision.gameObject.GetComponent<Player>();
                    p.takeDamage(damage);
                    this.gameObject.SetActive(false);
                    break;
                
                case "Projectile":
                    projectile = collision.gameObject.GetComponent<Projectile>();
                    this.takeDamage(projectile.doDamage());
                    projectile.destroy();                    
					break;

                case "Towers":
					tower = collision.gameObject.GetComponent<Tower>();
                    isMoving = false;
					break;
	
				case "Enemy":
					e = collision.gameObject.GetComponent<Enemy>();
                    isMoving = false;
					break;
        }
    }

    private void moving()
    {
        if (isMoving)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }
    
    private void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (!hasGivenCash)
            {
                player.GetComponent<Player>().updateCash(this.reward);
                hasGivenCash = true;
            }
            this.gameObject.SetActive(false);
        }
    }

    public int doDamage()
    {
        return this.damage;
    }

	public void resetEnemy(){
		health = originalHealth;
		e = null;
		tower = null;
		projectile = null;
		isMoving = true;
		hasGivenCash = false;
	}
}

