using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public float xMax, xMin, speed;
    float hor,x;

    void Update()
    {
       hor = Input.GetAxisRaw("Horizontal");
        
    }

    private void FixedUpdate()
    {
        x = Mathf.Clamp((transform.position.x + hor * speed), xMin, xMax);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
