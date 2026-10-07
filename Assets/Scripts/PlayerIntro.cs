using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIntro : MonoBehaviour
{
    Rigidbody2D rb;

    float moveH;
    float speed = 5f;
    float jump;
    bool isRun = false;

    Animator an;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        jump = rb.gravityScale * 8;
    }

    void Update()
    {
        an.SetBool("isRun", isRun);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocityX = moveH * speed;
    }


    private void OnMove(InputValue value)
    {
        moveH = value.Get<Vector2>().x;
        isRun = (moveH != 0);
    }

    private void OnJump(InputValue value)
    {
        rb.linearVelocityY = jump;
    } 

    /*
    public void Move(InputAction.CallbackContext ctx)
    {
        moveH = ctx.ReadValue<Vector2>().x;
        Debug.Log("moveH=" + moveH);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        rb.linearVelocityY = jump;
    }
    */
}
