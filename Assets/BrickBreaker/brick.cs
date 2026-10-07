using System;
using UnityEngine;

public class brick : MonoBehaviour
{
    public event Action OnDestroy;

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnDestroy.Invoke();
        Destroy(gameObject);
    }
}
