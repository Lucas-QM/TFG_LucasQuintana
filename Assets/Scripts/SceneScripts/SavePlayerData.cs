using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SavePlayerData : MonoBehaviour
{
    public Transform mainCamera;
    private float saveTime, save = 5f;
    private int BGToSave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && saveTime < Time.time)
        {
            DataManager.instance.HealthData(collision.GetComponent<PlayerHealth>().hp);
            DataManager.instance.ManaData(collision.GetComponent<PlayerHealth>().mana);
            if (mainCamera.GetChild(0).gameObject.activeSelf) BGToSave = 0; else BGToSave = 1;
            DataManager.instance.SavePlayerPosition(collision.transform.position.x, collision.transform.position.y, SceneManager.GetActiveScene().buildIndex, BGToSave);
            saveTime = Time.time + save;
        }
    }
}
