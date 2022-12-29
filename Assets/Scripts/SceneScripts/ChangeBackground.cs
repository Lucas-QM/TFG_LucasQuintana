using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public GameObject background1, background2, camera;
    public bool goUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !goUp)
        {
            camera.transform.GetChild(0).gameObject.SetActive(false);
            camera.transform.GetChild(1).gameObject.SetActive(true);
        } else if(collision.CompareTag("Player") && goUp)
        {
            camera.transform.GetChild(1).gameObject.SetActive(false);
            camera.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
