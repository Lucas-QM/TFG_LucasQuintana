using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isStatic, isWalker, isPatrol, isSearcher, isFollower, isMoverShooter, isFlying, shouldWait, isSniper, hasAnimation;
    public float timeToWait, shootingRange, lineOfSite, fireRate = 1f, nextFireTime;
    public GameObject bullet, bulletParent;
    public LayerMask whatIsGround;
    public Transform wallCheck, pitCheck, groundCheck, pointA, pointB;

    private bool walksRight, isWaiting, wallDetected, pitDetected, isGrounded, goToA, goToB;
    private float speed, checkDetectionRadius = 0.1f;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        goToA = true;
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFlying)
        {
            pitDetected = !Physics2D.OverlapCircle(pitCheck.position, checkDetectionRadius, whatIsGround);
            wallDetected = Physics2D.OverlapCircle(wallCheck.position, checkDetectionRadius, whatIsGround);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDetectionRadius, whatIsGround);

            if (pitDetected || wallDetected && isGrounded)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isStatic) StaticMovement();
        if (isWalker) WalkerMovement();
        if(isPatrol) PatrolMovement();
        if (isFollower) FollowerMovement();
        if(isSearcher) SearcherMovement();
        if (isMoverShooter) MooverShooterMovement();
        if (isSniper) SniperMovement();
    }

    private void StaticMovement()
    {
        anim.SetBool("Idle", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void WalkerMovement()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (hasAnimation)
        {
            anim.SetBool("Idle", false);
        }
        if (!walksRight)
        {
            rb.velocity = new Vector2(-speed + Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed + Time.deltaTime, rb.velocity.y);
        }
    }

    private void PatrolMovement()
    {
        if (goToA)
        {
            if (!isWaiting)
            {
                if (hasAnimation)
                {
                    anim.SetBool("Idle", false);
                }
                rb.velocity = new Vector2(-speed + Time.deltaTime, rb.velocity.y);
            }

            if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiting());
                }
                Flip();
                goToA = false;
                goToB = true;
            }
        }

        if (goToB)
        {
            if (!isWaiting)
            {
                if (hasAnimation)
                {
                    anim.SetBool("Idle", false);
                }
                rb.velocity = new Vector2(speed + Time.deltaTime, rb.velocity.y);
            }

            if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
            {
                if (shouldWait)
                {
                    StartCoroutine(Waiting());
                }
                Flip();
                goToA = true;
                goToB = false;
            }
        }
    }

    private void FollowerMovement()
    {
        if (getPlayerDistance() < lineOfSite)
        {
            LookAtPlayer();
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void SearcherMovement()
    {
        if (getPlayerDistance() < lineOfSite)
        {
            isPatrol = false;
            speed = 2.5f;
            isFollower = true;
        }
        else
        {
            isFollower = false;
            speed = 1;
            isPatrol = true;
        }
    }

    private void MooverShooterMovement()
    {
        if (getPlayerDistance() < lineOfSite && getPlayerDistance() > shootingRange)
        {
            LookAtPlayer();
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (getPlayerDistance() < shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void SniperMovement()
    {
        if (getPlayerDistance() < shootingRange)
        {
            anim.SetBool("Idle", false);
            LookAtPlayer();
            if (nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            anim.SetBool("Idle", true);
        }
    }

    private float getPlayerDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    private void LookAtPlayer()
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
    }

    IEnumerator Waiting()
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(timeToWait);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();
    }

    public void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
