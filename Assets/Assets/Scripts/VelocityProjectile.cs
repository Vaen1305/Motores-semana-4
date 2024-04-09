using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f; // Velocidad del proyectil

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
