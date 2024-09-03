using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public bool isLeft = false;
    public Animator animasi;
    public GameObject fireball;
    public GameObject firepos;
    public AudioClip slashSound;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D;
    void Start()
    {
        // walk = Vector3.zero;
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        rb.gravityScale = 1f;
        boxCollider2D.isTrigger = false;
        Walk();
        Jump();
        Attack();
    }

    void Attack()
    {
        //Slash
        if (Input.GetMouseButtonDown(0))
        {
            rb.gravityScale = 0f;
            boxCollider2D.isTrigger = true;
            animasi.SetBool("isSlashing", true);
            audioSource.clip = slashSound;
            if (isGrounded)
            {
                audioSource.Play();
            }
            StartCoroutine(DelaySlash());
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animasi.SetBool("isStriking", true);
            StartCoroutine(DelayStrike());
        }
    }

    IEnumerator DelaySlash()
    {
        yield return new WaitForSeconds(0.5f);
        animasi.SetBool("isSlashing", false);
    }

    IEnumerator DelayStrike()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject spawnedFire = (GameObject)Instantiate(fireball);
        spawnedFire.transform.position = firepos.transform.position;
        animasi.SetBool("isStriking", false);
    }

    void Walk()
    {
        // Walk
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            animasi.SetBool("isWalking", true);
        }
        else
        {
            animasi.SetBool("isWalking", false);
        }

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isLeft = false;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isLeft = true;
        }
    }
    void Jump()
    {
        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (!isGrounded && Input.GetMouseButtonDown(0))
        {
            animasi.SetBool("isSlashing", true);
            Debug.Log("jump slashing");
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            animasi.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            animasi.SetBool("isJumping", true);
        }
    }
}