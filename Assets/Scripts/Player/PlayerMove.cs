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

        if (Input.GetButtonDown("Jump"))//���� ����
        {
            if (IsGrounded())//ù ��° ����
            {
                Debug.Log("D");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (isDoubleJumping && addJump > 0)//N��° ����
            {
                addJump--;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)//���� ª�� �ϱ�
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

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);//�⺻ �̵�
    }

    private bool IsGrounded()//�ٴ� üũ
    {
        
        bool temp = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (temp) 
        {
            addJump = 1;
        }
            

        return temp;
    }

    private void Flip()//������ �� �� üũ
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

        if (Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundLayer))//���� ������ �� �������� �� ��
        {
            horizontal = 0;
        }
    }

    private IEnumerator Dash()//�뽬
    {
        canDash = false;
        dashNow = true;

        float originalGravity = rb.gravityScale;//���� �߷�
        rb.gravityScale = 0f;//�߷� ����

        rb.AddForce(new Vector2(transform.localScale.x * dashPower, 0), ForceMode2D.Impulse);//�뽬

        camFollow.smoothSpeed = 0.5f;
        trail.Play();//�ܻ� �ѱ�
        yield return new WaitForSeconds(dashTime);

        camFollow.smoothSpeed = 3f;
        rb.gravityScale = originalGravity;//�߷� ���󺹱�

        dashNow = false;

        yield return new WaitForSeconds(dashTime);
        camFollow.smoothSpeed = 1f;

        yield return new WaitForSeconds(dashCooldown);//�뽬 ��Ÿ��
        canDash = true;
    }
}

