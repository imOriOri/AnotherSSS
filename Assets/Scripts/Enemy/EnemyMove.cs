using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    int nextMove;

    public float moveSpeed;
    public Transform target;
    bool isTracing;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);
    }

    private void FixedUpdate() 
    {
        rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if(rayHit.collider== null)
        {
            Turn();
        }
    }

    public void Think()
    {
        nextMove = Random.Range(-1, 2);
        if(nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == 1);

        CancelInvoke();
        Invoke("Think", 2);
    }

        private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            CancelInvoke("Think"); 
            if(target.position.x > transform.position.x) {
                nextMove = 3;
            } else if (target.position.x < transform.position.x) {
                nextMove = -3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player")
            Think();
    }
}
