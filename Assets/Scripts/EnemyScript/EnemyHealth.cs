using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public bool isDamaged;
    public GameObject deathEffect;
    private SpriteRenderer sprite;
    Blink blink;
    Rigidbody2D rb;
    public float originalHealth;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        blink = GetComponent<Blink>();
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon") && !isDamaged)
        {
            enemy.hp -= 2f;
            if(collision.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            } else
            {
                rb.AddForce(new Vector2(-enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            }

            StartCoroutine(Damager());
            if(enemy.hp <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);

                if (enemy.shouldRespawn)
                {
                    transform.GetComponentInParent<RespawnScript>().StartCoroutine(GetComponentInParent<RespawnScript>().RespawnEnemy());
                } else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    IEnumerator Damager()
    {
        isDamaged = true;
        sprite.material = blink.blink;
        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
        sprite.material = blink.original;
    }
}
