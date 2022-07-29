using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("???? ?? ??")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float jumpSpeed;
        
    private float applySpeed;

    [Header("???? ??? ??")]
    public bool isDoubleJumping = false;
    public bool isDashing = false;

    int jumpCount = 2;
    bool isJumping = false;
    bool isRunning = false;

    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        applySpeed = walkSpeed;
    }

    void Update()
    {
        Moving();
        Jumping();
        Running();
        Walking();
    }

    //???? ??
    private void Moving()
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

        if (rigid.velocity.x > applySpeed)
        {
            rigid.velocity = new Vector2(applySpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -applySpeed)
        {
            rigid.velocity = new Vector2(-applySpeed, rigid.velocity.y);
        }

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.2f, rigid.velocity.y);
        }
    }

    //???? ??
    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                if(jumpCount > 0)
                {
                    if (isDoubleJumping)
                    {
                        rigid.AddForce(Vector3.up * jumpSpeed * 100f);
                        jumpCount--;
                    }
                    else if (!isDoubleJumping)
                    {
                        isJumping = true;
                        rigid.AddForce(Vector3.up * jumpSpeed * 100f);
                    }
                }
            }
        }
    }

    //???? ???
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applySpeed = runSpeed;
        }
    }

    //???? ??
    private void Walking()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            applySpeed = walkSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform")
        {
            isJumping = false;
            jumpCount = 2; 
        }
    }
}

