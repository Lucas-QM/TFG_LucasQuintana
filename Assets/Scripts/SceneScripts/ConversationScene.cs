using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationScene : MonoBehaviour
{
    public GameObject platform;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.GetComponentInParent<PlayerController>().isTalking)
        {
            platform.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            platform.SetActive(true);
        }
    }
}
