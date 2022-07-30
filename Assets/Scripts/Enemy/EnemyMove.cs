using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    public float ChangeMoveSpeed;

    Vector3 movement;
    int movementFlag = 0;

    private void Start()
    {
        StartCoroutine(ChangeMovement());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * moveSpeed * Time.deltaTime;
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);
        yield return new WaitForSeconds(ChangeMoveSpeed);

        StartCoroutine(ChangeMovement());
    }

}
