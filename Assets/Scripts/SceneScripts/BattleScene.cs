using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    public GameObject enemies, door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetActive(true);
            enemies.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (enemies.transform.childCount == 0)
        {
            door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
