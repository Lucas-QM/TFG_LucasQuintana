using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubWeapons : MonoBehaviour
{

    public float ManaCost, damageToGive;
    public GameObject magic;

    // Update is called once per frame
    void Update()
    {
        UseSubWeapon();
    }

    public void UseSubWeapon()
    {
        if (Input.GetButtonDown("Fire2") && transform.GetComponentInChildren<PlayerHealth>().mana > 0)
        {
            transform.GetComponentInChildren<PlayerHealth>().mana -= ManaCost;
            GameObject subItem = Instantiate(magic, transform.position, Quaternion.identity);
            //AudioManager.instance.PlayAudio(AudioManager.instance.spark);
            if(transform.localScale.x < 0)
            {
                subItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(-800f, 0f), ForceMode2D.Force);
            } else
            {
                subItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(800f, 0f), ForceMode2D.Force);
            }
        }
    }
}
