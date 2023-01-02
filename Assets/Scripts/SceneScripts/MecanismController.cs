using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanismController : MonoBehaviour
{
    public int numberOfLevers;
    public bool hasToActivate, hasToDesactivate;
    public int leversActive;
    public GameObject objectToActivate, objectToDesactivate;

    // Update is called once per frame
    void Update()
    {
        if(leversActive == numberOfLevers)
        {
            if (hasToActivate)
            {
                objectToActivate.SetActive(true);
            }

            if (hasToDesactivate)
            {
                objectToDesactivate.SetActive(false);
            }
        }
    }
}
