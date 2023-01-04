using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    private float speed;
    Rigidbody2D bulletRB;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Enemy>().speed;
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position -transform.position).normalized * speed;
        if(target.transform.position.x < transform.position.x)
        {
            transform.localScale *= new Vector2(-1, transform.localScale.y);
        }
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
