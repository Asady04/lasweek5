using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            if (playerController.isLeft)
            {
                transform.Rotate(0, 180, 0);
                rb.velocity = transform.right * speed;

            }
            else
            {
                rb.velocity = transform.right * speed;
            }
        }
    }

    void Update()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > max.x || transform.position.x < max.x * -1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
