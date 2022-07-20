using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Product
{
    [SerializeField] private float speed;
    
    [SerializeField] private int damage;

    [SerializeField] private int health;
    public GameObject player;

    private Player p;

    private Arrow arrow;

    private Barrel barrel;

    private Cannon cannon;

	private Tower tower;

    private Gauss gauss;

	private Enemy e;
    public int reward;

    private bool isMoving = true;

    void Start()
    {
        Physics.IgnoreLayerCollision(1,5);
    }
    
    void Update()
    {
		if(e != null){
			isMoving = true;
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
                    Destroy(this.gameObject);
                    break;
                
                case "Arrow":
                    arrow = collision.gameObject.GetComponent<Arrow>();
                    this.takeDamage(arrow.doDamage());
                    arrow.destroy();                    
					break;

                case "Barrel":
                    barrel = collision.gameObject.GetComponent<Barrel>();
                    this.takeDamage(barrel.doDamage());
                    barrel.destroy();                    
					break;

                case "Cannon":
                    cannon = collision.gameObject.GetComponent<Cannon>();
                    this.takeDamage(cannon.doDamage());
                    cannon.destroy();                    
					break;

                case "Gauss":
                    gauss = collision.gameObject.GetComponent<Gauss>();
                    this.takeDamage(gauss.doDamage());
                    gauss.destroy();                    
					break;

				case "Towers":
					isMoving = false;
					break;
	
				case "Enemy":
					e = collision.gameObject.GetComponent<Enemy>();
					isMoving = false;
					break;
        }
    }

	public void setIsMoving(){
			isMoving = true;
	}

	public bool getIsMoving(){
			return isMoving;
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
            player.GetComponent<Player>().updateCash(this.reward);
            Destroy(this.gameObject);
        }
    }

    public int doDamage()
    {
        return this.damage;
    }
}

