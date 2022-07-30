using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 1;
    public Vector2 offset;
    public float limitMinX, limitMaxX;

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            -10);

        Vector3 modified = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        modified.x = Mathf.Clamp(modified.x, limitMinX, limitMaxX);

        transform.position = modified;//맵 끝이 느리게 보임 해결
    }
}
