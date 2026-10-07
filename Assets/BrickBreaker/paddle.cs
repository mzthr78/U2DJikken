using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class paddle : MonoBehaviour
{
    Rigidbody2D rb;

    float moveH = 0;
    float speed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // たぶんこれは旧式InputSystem
        moveH = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        //rb.AddForce(new Vector2(moveH, 0), ForceMode2D.Impulse); 
        rb.linearVelocityX = moveH * speed; // こっちのほうがいいかな
    }

    /*
    private void OnMove(InputValue value)
    {
        moveH = value.Get<Vector2>().x;
    }

    private void OnJump(InputValue value)
    {
        Debug.Log("hoge!");
    }
    */
}
