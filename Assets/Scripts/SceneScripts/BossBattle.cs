using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public GameObject boss, bossUI;
    public GameObject[] objectsToActivate, objectsToDesactivate;
    public bool hasToActivate, hasToDesactivate;

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            bossUI.SetActive(false);
            if (hasToActivate)
            {
                activate_deactivate(false, objectsToActivate);
            }
            if (hasToDesactivate)
            {
                activate_deactivate(true, objectsToDesactivate);
            }
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            bossUI.SetActive(true);

            if (hasToActivate)
            {
                activate_deactivate(true, objectsToActivate);
            }
            if (hasToDesactivate)
            {
                activate_deactivate(false, objectsToDesactivate);
            }
        }
    }

    private void activate_deactivate(bool option, GameObject[] gameObjects)
    {
        foreach(GameObject go in gameObjects)
        {
            go.SetActive(option);
        }
    }
}
