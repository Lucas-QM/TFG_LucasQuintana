using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4BossMovement : MonoBehaviour
{
    Animator anim;

    public int attacksBeforeSpecial;
    public float cdBetweenAttacks, lineOfSite, shootingRange;
    public bool doSpecialAttack, walksRight, waitingAttack;
    public GameObject bullet, bulletParent;

    private Transform player;
    private int attacksMade;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        doSpecialAttack = false;
        waitingAttack = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = GetComponent<Enemy>().speed;
    }

    // Update is called once per frame
    void Update()
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

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (attacksMade < attacksBeforeSpecial && !doSpecialAttack)
        {
            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            if (!waitingAttack)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                attacksMade++;
                StartCoroutine(WaitingAttack());
            }
        } else
        {
            attacksMade = 0;
            print("fresco");
            //doSpecialAttack = true;
        }
    }

    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    IEnumerator WaitingAttack()
    {
        waitingAttack = true;
        yield return new WaitForSeconds(cdBetweenAttacks);
        waitingAttack = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
