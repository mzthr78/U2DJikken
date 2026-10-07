using System;
using UnityEngine;

public class abyss : MonoBehaviour
{
    public event Action OnFall;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Ball")
        {
            OnFall.Invoke();
        }
    }
}
