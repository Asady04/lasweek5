using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animasi;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0, 180, 0);
        rb.velocity = transform.right * speed;
        animasi.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            animasi.SetBool("isDying", true);
            rb.velocity = Vector3.zero;
            StartCoroutine(DelaytoDeath());

        }

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < max.x * -1)
        {
            transform.position = new Vector2(max.x, transform.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetMouseButtonDown(0))
            {
                animasi.SetBool("isHurting", true);
                animasi.SetBool("isWalking", false);
                rb.velocity = Vector3.zero;
                health -= 1;
                StartCoroutine(DelaytoWalk());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fire")
        {
            animasi.SetBool("isHurting", true);
            animasi.SetBool("isWalking", false);
            rb.velocity = Vector3.zero;
            health -= 1;
            StartCoroutine(DelaytoWalk());
        }
    }

    IEnumerator DelaytoWalk()
    {
        yield return new WaitForSeconds(0.2f);

        animasi.SetBool("isHurting", false);
        animasi.SetBool("isWalking", true);

        rb.velocity = transform.right * speed;
    }

    IEnumerator DelaytoDeath()
    {
        yield return new WaitForSeconds(1.2f);

        Destroy(gameObject);
    }
}
