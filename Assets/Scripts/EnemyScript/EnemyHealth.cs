using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public bool isDamaged, hasHealthBar;
    public GameObject deathEffect;
    private SpriteRenderer sprite;
    Blink blink;
    Rigidbody2D rb;
    public float originalHealth;
    public AudioSource damageSound;
    public GameObject potionRed, potionBlue, potionYellow;
    public Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        blink = GetComponent<Blink>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (hasHealthBar)
        {
            healthImage.fillAmount = enemy.hp / originalHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon") || collision.CompareTag("Magic") && !isDamaged)
        {
            DamageReceived(collision);
            //AudioManager.instance.PlayAudio(damageSound);
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
                float chance = Random.Range(1f, 100f);
                if ( chance < 75)
                {
                    float potionChance = Random.Range(1f, 100f);
                    if(potionChance < 50)
                    {
                        Instantiate(potionRed, transform.position, Quaternion.identity);
                    } else if(potionChance > 50 && potionChance < 100)
                    {
                        Instantiate(potionBlue, transform.position, Quaternion.identity);
                    } else
                    {
                        Instantiate(potionYellow, transform.position, Quaternion.identity);
                    }
                }

                if (enemy.shouldRespawn)
                {
                    transform.GetComponentInParent<RespawnScript>().StartCoroutine(GetComponentInParent<RespawnScript>().RespawnEnemy());
                } else
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    private void DamageReceived(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            print(collision.GetComponentInParent<PlayerController>().damageToGive + "golpe melee");
            enemy.hp -= collision.GetComponentInParent<PlayerController>().damageToGive;
        } else
        {
            print(collision.GetComponent<Magic>().damageToGive + "golpe magico");
            enemy.hp -= collision.GetComponent<Magic>().damageToGive;
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
