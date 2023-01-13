using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight, damageToGive;
    Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    Animator anim;
    public bool isTalking;

    private bool isAttacking, isGrounded;
    private float velX, velY;

    public static PlayerController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isAttacking = false;
        if(PlayerPrefs.GetInt("ActualScene") == SceneManager.GetActiveScene().buildIndex)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        if (!isTalking)
        {
            Movement();
            FlipCharacter();
            Jump();
            Attack();
        }
    }

    public void Attack()
    {
        if(Input.GetButton("Fire1") && !isAttacking)
        {
            StartCoroutine(Attacking());
        }
    }

    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;

        rb.velocity = new Vector2(velX * speed, velY);

        if(rb.velocity.x != 0)
        {
            anim.SetBool("Run", true);
        } else
        {
            anim.SetBool("Run", false);
        }
    }

    public void Jump()
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void FlipCharacter()
    {
        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator Attacking()
    {
        isAttacking = true;
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.20f);
        anim.SetBool("Attack", false);
        isAttacking = false;
    }
}
