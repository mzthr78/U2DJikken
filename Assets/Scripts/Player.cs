using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform footPos;
    [SerializeField] LayerMask groundLayer;

    private float moveH;
    private float moveV;

    float speed = 5f;
    float jumpower = 7.8f;

    Boolean isGrounded = false;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(footPos.position, new Vector2(1.0f, 0.1f), 0f, groundLayer);
        debugText.text = "isGrounded = " + isGrounded;

        //rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
        rb.linearVelocityX = moveH * speed;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveH = ctx.ReadValue<Vector2>().x;
        moveV = ctx.ReadValue<Vector2>().y;

        Debug.Log(new Vector2(moveH, moveV));
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.linearVelocityY = jumpower;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }

    /*
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, 0.5f);
    }
    */
}
