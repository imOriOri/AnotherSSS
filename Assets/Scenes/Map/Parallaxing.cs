using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    Transform cam;
    public float parallaxEffect;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void FixedUpdate()
    {
        float dist = (cam.position.x * parallaxEffect);

        transform.position = new Vector3(dist, transform.position.y, transform.position.z);
    }
}
