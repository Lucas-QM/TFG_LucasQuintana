using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsScript : MonoBehaviour
{
    public bool recoverHealth, recoverMana;
    public int healthToRecover, manaToRecover;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.potion.Play();
            if (recoverHealth)
            {
                collision.GetComponent<PlayerHealth>().hp += healthToRecover;
            }
            if (recoverMana)
            {
                collision.GetComponent<PlayerHealth>().mana += manaToRecover;
            }
            Destroy(gameObject);
        }
    }
}
