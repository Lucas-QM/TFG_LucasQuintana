using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public Texts texts;
    public bool isInteractuable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInteractuable)
        {
            if (collision.CompareTag("Weapon"))
            {
                FindObjectOfType<ControlDialoge>().ActiveDialoge(texts);
            }
        } else
        {
            if (collision.CompareTag("Player"))
            {
                FindObjectOfType<ControlDialoge>().ActiveDialoge(texts);
            }
        }
        
    }
}
