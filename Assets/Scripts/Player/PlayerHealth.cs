using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float hp;
    public float maxHealth;
    public float mana, maxMana;
    public Image healthImage;
    public Image manaImage;
    bool isInmune;
    public float inmunityTime;
    SpriteRenderer sprite;
    Blink material;
    public float knockbackForceX;
    public float knockbackForceY;
    Rigidbody2D rb;
    public GameObject gameOverImg;

    // Start is called before the first frame update
    void Start()
    {
        gameOverImg.SetActive(false);
        rb = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponentInParent<SpriteRenderer>();
        material = GetComponentInParent<Blink>();
        hp = PlayerPrefs.GetFloat("Health", maxHealth);
        mana = PlayerPrefs.GetFloat("Mana", maxMana);
        material.original = sprite.material;
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = hp / 100;
        manaImage.fillAmount = mana / 100;
        if (hp > maxHealth)
        {
            hp = maxHealth;
        }
        if(mana > maxMana)
        {
            mana = maxMana;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Projectile") && !isInmune)
        {
            hp -= collision.GetComponent<Enemy>().damageToGive;
            //AudioManager.instance.playerDamage.Play();
            StartCoroutine(Inmunity());

            if(collision.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            } else
            {
                rb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }

            if(hp <= 0)
            {
                Time.timeScale = 0;
                gameOverImg.SetActive(true);
                AudioManager.instance.BGMusic.Stop();
            }
        }
    }

    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}
