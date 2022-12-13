using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float secondsDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, secondsDestroy);
    }
}
