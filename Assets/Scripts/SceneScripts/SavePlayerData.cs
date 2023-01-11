using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerData : MonoBehaviour
{
    private float saveTime, save = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && saveTime < Time.time)
        {
            DataManager.instance.HealthData(collision.GetComponent<PlayerHealth>().hp);
            DataManager.instance.ManaData(collision.GetComponent<PlayerHealth>().mana);
            DataManager.instance.SavePlayerPosition(collision.transform.position.x, collision.transform.position.y, SceneManager.GetActiveScene().buildIndex);
        }
    }
}
