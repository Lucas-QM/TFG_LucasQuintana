using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4BossMovement : MonoBehaviour
{
    Animator anim;

    public int attacksBeforeSpecial;
    public float cdBetweenAttacks, lineOfSite, shootingRange;
    public bool doSpecialAttack, walksRight, waitingAttack, preparingAttack, doingSpecial;
    public GameObject bullet, bulletParent, positionSpecial;

    private Transform player;
    private int attacksMade;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        doSpecialAttack = false;
        waitingAttack = false;
        preparingAttack = true;
        doingSpecial = false;
        anim = GetComponent<Animator>();
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
                //Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                print(attacksMade + "ATACA");
                attacksMade++;
                StartCoroutine(WaitingAttack());
            }
        } else
        {
            attacksMade = 0;
            doSpecialAttack = true;
            if (preparingAttack)
            {
                transform.position = positionSpecial.transform.position;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
            if (!doingSpecial)
            {
                print("entra al if para el especial");
                StartCoroutine(SpecialAttack());
            }
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

    IEnumerator SpecialAttack()
    {
        doingSpecial = true;
        print("entra al ataque especial");
        anim.SetBool("Breath", true);
        yield return new WaitForSeconds(0.8f);
        preparingAttack = false;
        print(preparingAttack + " Debería dejar de seguir el punto");
        yield return new WaitForSeconds(0.7f);
        doingSpecial = false;
        doSpecialAttack = false;
        preparingAttack = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        anim.SetBool("Breath", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
