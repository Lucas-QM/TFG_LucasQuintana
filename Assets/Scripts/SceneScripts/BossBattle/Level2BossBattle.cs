using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossBattle : MonoBehaviour
{
    public GameObject boss, platform, toNextLevel, bossUI;

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            platform.SetActive(true);
            toNextLevel.SetActive(false);
            bossUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            bossUI.SetActive(true);
        }
    }
}
