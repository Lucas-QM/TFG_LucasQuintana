using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDesactivation : MonoBehaviour
{
    public GameObject ObjectToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            ObjectToActivate.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
