using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CameraFollow camFollow;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    public bool isDoubleJumping = false;
    public bool isDashing = false;

    public int addJump = 1;

    bool canDash = true;
    bool dashNow;
    private float dashPower = 12f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;


    [SerializeField] private ParticleSystem trail;
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        groundLayer = ~groundLayer;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dashNow)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        if (Input.GetButtonDown("Jump"))//점프 시작
        {
            if (IsGrounded())//첫 번째 점프
            {
                Debug.Log("D");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (isDoubleJumping && addJump > 0)//N번째 점프
            {
                addJump--;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)//점프 짧게 하기
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (isDashing) 
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashNow)
            return;

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);//기본 이동
    }

    private bool IsGrounded()//바닥 체크
    {
        
        bool temp = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (temp) 
        {
            addJump = 1;
        }
            

        return temp;
    }

    private void Flip()//뒤집기 및 벽 체크
    {
        if (dashNow)
            return;

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        Vector2 boxSize = new Vector2(1,0.9f);

        if (Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundLayer))//벽이 있으면 그 방향으로 못 감
        {
            horizontal = 0;
        }
    }

    private IEnumerator Dash()//대쉬
    {
        canDash = false;
        dashNow = true;

        float originalGravity = rb.gravityScale;//원래 중력
        rb.gravityScale = 0f;//중력 제거

        rb.AddForce(new Vector2(transform.localScale.x * dashPower, 0), ForceMode2D.Impulse);//대쉬

        camFollow.smoothSpeed = 0.5f;
        trail.Play();//잔상 켜기
        yield return new WaitForSeconds(dashTime);

        camFollow.smoothSpeed = 3f;
        rb.gravityScale = originalGravity;//중력 원상복구

        dashNow = false;

        yield return new WaitForSeconds(dashTime);
        camFollow.smoothSpeed = 1f;

        yield return new WaitForSeconds(dashCooldown);//대쉬 쿨타임
        canDash = true;
    }
}

