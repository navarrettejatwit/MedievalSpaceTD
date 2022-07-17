using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 0;

    [SerializeField] private int damage = 0;
    
    void FixedUpdate()
    {
        this.transform.Translate(new Vector3(1f, 0f, 0f) * speed * Time.deltaTime, null);
    }

    private void destroy()
    {
        Destroy(this);
    }

    public int doDamage()
    {
        return damage;
    }
}
