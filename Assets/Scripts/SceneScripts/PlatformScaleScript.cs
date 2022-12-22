using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScaleScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.GetComponent<ObjectMovement>().willDestroy)
            {
                gameObject.GetComponent<ObjectMovement>().startCd = true;
            }
            collision.transform.parent.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent.SetParent(null);
        }
    }
}
