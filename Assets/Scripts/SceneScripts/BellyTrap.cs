using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellyTrap : MonoBehaviour
{
    public float detectionRadius, fireRate, nextFireTime;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < detectionRadius && nextFireTime < Time.time)
        {
            StartCoroutine(Breath());
        }
    }

    IEnumerator Breath()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        nextFireTime = Time.time + fireRate;
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
