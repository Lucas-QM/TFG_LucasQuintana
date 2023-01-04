using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Level2BossMovement : MonoBehaviour
{
    public float lineOfSite, cdBetweenAttacks;
    private Transform player;
    public GameObject[] positions;
    public GameObject bullet;
    public GameObject bulletParent;

    public int attacksBeforeTp;
    private int attacksMade;
    private bool walksRight, isWaiting;
    // Start is called before the first frame update
    void Start()
    {
        isWaiting = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        teleportation();
    }

    private void FixedUpdate()
    {
        if (player.position.x > transform.position.x)
        {
            if (!walksRight)
            {
                Flip();
            }
        }
        else
        {
            if (walksRight)
            {
                Flip();
            }
        }

        if (attacksMade < attacksBeforeTp && !isWaiting)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if(distanceFromPlayer < lineOfSite)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                attacksMade++;
                StartCoroutine(WaitBetweenAttacks());
            }
        } else if(attacksMade == attacksBeforeTp)
        {
            attacksMade = 0;
            teleportation();
        }
    }

    IEnumerator WaitBetweenAttacks()
    {
        print(cdBetweenAttacks + "Espera de ataque");
        isWaiting = true;
        yield return new WaitForSeconds(cdBetweenAttacks);
        isWaiting = false;
    }

    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    public void teleportation()
    {
        int nextPosition = Random.Range(0, positions.Length);
        transform.position = positions[nextPosition].transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
