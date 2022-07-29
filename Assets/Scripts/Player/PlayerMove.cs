using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    bool isJumping = false;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpSpeed * 100f);
            }
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (rigid.velocity.x > moveSpeed)
        {
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -moveSpeed)
        {
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
        }
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            isJumping = false;
        }
    }
}

