using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isPatrol;
    public bool isSearcher;
    public bool isFollower;
    public bool isMoverShooter;
    public bool isFlying;
    public bool walksRight;
    public bool shouldWait;
    public bool isWaiting;
    public bool isSniper;
    public bool hasAnimation;
    public float timeToWait;

    public Transform wallCheck, pitCheck, groundCheck;
    bool wallDetected, pitDetected, isGrounded;
    public float detectionRadius;
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 1f;
    public float nextFireTime;
    public LayerMask whatIsGround;

    public Transform pointA, pointB;
    bool goToA, goToB;

    public float lineOfSite;
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
            pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, whatIsGround);
            wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGround);

            if (pitDetected || wallDetected && isGrounded)
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (hasAnimation)
            {
                anim.SetBool("Idle", false);
            }
            if (!walksRight)
            {
                rb.velocity = new Vector2(-speed + Time.deltaTime, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(speed + Time.deltaTime, rb.velocity.y);
            }
        }
        if(isPatrol)
        {
            if (goToA)
            {
                if (!isWaiting)
                {
                    if(hasAnimation)
                    {
                        anim.SetBool("Idle", false);
                    }
                    rb.velocity = new Vector2(-speed + Time.deltaTime, rb.velocity.y);
                }

                if(Vector2.Distance(transform.position, pointA.position) < 0.2f)
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
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (isFollower)
        {
            if (distanceFromPlayer < lineOfSite)
            {
                if(player.position.x > transform.position.x)
                {
                    if (!walksRight)
                    {
                        Flip();
                    }
                } else
                {
                    if (walksRight)
                    {
                        Flip();
                    }
                }
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
        }

        if(isSearcher)
        {
            if(distanceFromPlayer < lineOfSite)
            {
                isPatrol = false;
                speed = 2.5f;
                isFollower = true;
            } else
            {
                isFollower = false;
                speed = 1;
                isPatrol = true;
            }
        }

        if (isMoverShooter)
        {
            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
            {
                if (player.position.x > transform.position.x)
                {
                    if (!walksRight)
                    {
                        Flip();
                    }
                } else
                {
                    if (walksRight)
                    {
                        Flip();
                    }
                }
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            } else if(distanceFromPlayer < shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
        }
        if (isSniper)
        {
            if (distanceFromPlayer < shootingRange)
            {
                anim.SetBool("Idle", false);
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
                if(nextFireTime < Time.time)
                {
                    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                    nextFireTime = Time.time + fireRate;
                }
            } else
            {
                anim.SetBool("Idle", true);
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
