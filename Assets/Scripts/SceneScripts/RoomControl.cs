using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject enemies;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CameraController.instance.activeRoom = transform.GetChild(0);
            if(enemies != null)
            {
                enemies.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraController.instance.activeRoom = transform.GetChild(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            if (enemies != null)
            {
                enemies.SetActive(false);
            }
        }
    }
}
