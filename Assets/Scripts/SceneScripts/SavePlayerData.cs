using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerData : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("entra");
        DataManager.instance.HealthData(collision.GetComponent<PlayerHealth>().hp);
        DataManager.instance.ManaData(collision.GetComponent<PlayerHealth>().mana);
    }
}
