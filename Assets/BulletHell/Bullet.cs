using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 dir = Vector3.zero;
    //public String tag = "";

    void Start()
    {
    }

    void FixedUpdate()
    {
        transform.position += dir.normalized * Time.deltaTime * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
