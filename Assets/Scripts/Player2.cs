using System;
using System.IO.Compression;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    Rigidbody2D rb;
    LayerMask groundLayer = 64;
    SpriteRenderer sr;
    Sprite[] sp; // = Resources.LoadAll<Sprite>("Resources/Sprite/Bokudoko_sprite"); //　ここでは呼べない
    Animator an;

    [SerializeField] TextMeshProUGUI debugText;

    float moveH;
    float speed = 5f;
    float jump = 8f;
    Boolean isGrounded = false;

    Boolean isRun = false;

    Vector3 offset = new Vector3(0, -0.5f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        an = GetComponent<Animator>();

        jump = rb.gravityScale * 8;

        /*
        // こういうこともできる
        sp = Resources.LoadAll<Sprite>("Sprite/bokudoko_sprite"); // "Resources"以下から書く
        sr.sprite = sp[4];
        */
    }

    void Update()
    {
        an.SetBool("isRun", isRun);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(transform.position + offset, new Vector2(1.0f, 0.1f), 0, groundLayer);
        debugText.text = isGrounded.ToString();
        rb.linearVelocityX = moveH * speed;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveH = ctx.ReadValue<Vector2>().x;
        Debug.Log("moveH=" + moveH);

        isRun = (moveH != 0);

        if (moveH < 0)
        {
            sr.flipX = true;
        }

        if (moveH > 0)
        {
            sr.flipX = false;
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
        {
            rb.linearVelocityY = jump;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offset, new Vector2(1.0f, 0.1f));
    }
}
