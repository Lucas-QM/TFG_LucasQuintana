using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3BossMovement : MonoBehaviour
{
    Animator anim;

    public float lineOfSite, fireRate, nextFireTime;
    public int attacksBeforeSpecial, numberOfSummons;
    public GameObject bullet, bulletBulletHell, bulletParent;
    public GameObject[] minionPositions, enemiesCanCreate, bulletParentSky;

    private Transform player;
    private int attacksMade;
    private bool walksRight, doingAnimation;

    // Start is called before the first frame update
    void Start()
    {
        doingAnimation = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
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

        if (attacksMade < attacksBeforeSpecial && !doingAnimation && nextFireTime < Time.time)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < lineOfSite)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                attacksMade++;
                nextFireTime = Time.time + fireRate;
            }
        }
        else if (attacksMade == attacksBeforeSpecial && !doingAnimation)
        {
            if (Random.Range(1, 10) < 7)
            {
                SummonMinions();
            }
            else
            {
                BulletHell();
            }
        }
    }

    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    private void SummonMinions()
    {
        StartCoroutine(DoAnimation("Breath", 0.84f));
        for (int i = 0; i < numberOfSummons; i++)
        {
            int position = Random.Range(0, minionPositions.Length);
            int minion = Random.Range(0, enemiesCanCreate.Length);
            Instantiate(enemiesCanCreate[minion], minionPositions[position].transform.position, Quaternion.identity);
        }
        attacksMade = 0;
    }

    private void BulletHell()
    {
        StartCoroutine(DoAnimation("Burn", 1.5f));
        foreach(GameObject go in bulletParentSky)
        {
            Instantiate(bulletBulletHell, go.transform.position, Quaternion.identity);
        }
    }

    IEnumerator DoAnimation(string anima, float animationTime)
    {
        doingAnimation = true;
        anim.SetBool(anima, true);
        yield return new WaitForSeconds(animationTime);
        anim.SetBool(anima, false);
        doingAnimation = false;
        attacksMade = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
