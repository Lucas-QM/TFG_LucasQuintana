using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed;
    public bool shouldMove, shouldWait, willDestroy, canContinue, startCd;
    public float timeToWait, timeToDestroy, destroyCd;
    private bool moveToA, moveToB;

    // Start is called before the first frame update
    void Start()
    {
        canContinue = true;
        moveToA = true;
        moveToB = false;
        destroyCd = timeToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            MoveObject();
        }

        if (startCd)
        {
            destroyCd -= Time.deltaTime;
            if(destroyCd <= 0)
            {
                StartCoroutine(ReactivetePlatform());
                destroyCd = timeToDestroy;
                startCd = false;
            }
        }
    }

    IEnumerator ReactivetePlatform()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        BoxCollider2D[] myColliders = gameObject.GetComponents<BoxCollider2D>();

        foreach(var collider in myColliders)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(2f);

        foreach(var collider in myColliders)
        {
            collider.enabled = true;
        }
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void MoveObject()
    {
        float distanceToA = Vector2.Distance(transform.position, pointA.position);
        float distanceToB = Vector2.Distance(transform.position, pointB.position);

        if(distanceToA > 0.1f && moveToA)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if(distanceToA < 0.3f && canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                }
                moveToA = false;
                moveToB = true;
            }
        }

        if(distanceToB > 0.1f && moveToB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if(distanceToB < 0.3f && canContinue)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiter());
                }
                moveToA = true;
                moveToB = false;
            }
        }
    }

    IEnumerator Waiter()
    {
        shouldMove = false;
        canContinue = false;
        yield return new WaitForSeconds(timeToWait);
        shouldMove= true;
        canContinue = true;
    }
}
