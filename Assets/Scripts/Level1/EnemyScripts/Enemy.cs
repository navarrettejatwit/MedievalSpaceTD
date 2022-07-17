using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Product
{
    [SerializeField] private float speed;
    
    [SerializeField] private int damage;

    [SerializeField] private int health;

    private Player p;

    private Arrow arrow;

	private Tower tower;

	private Enemy e;

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
                    Destroy(arrow.gameObject);
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
            Destroy(this.gameObject);
        }
    }

    public int doDamage()
    {
        return this.damage;
    }
    
}
