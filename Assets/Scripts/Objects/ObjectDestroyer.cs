using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float secondsDestroy;

    void Start()
    {
        Destroy(gameObject, secondsDestroy);
    }
}