using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    [SerializeField] private int damage = 0;

    void Awake()
    {
        Physics.IgnoreLayerCollision(8,8);
    }

    void FixedUpdate()
    {
        this.transform.Translate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime, null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Wall")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void destroy()
    {
        this.gameObject.SetActive(false);
    }

    public int doDamage()
    {
        return damage;
    }
}
