using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Referenses")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private int bulletDmg = 1;

    public Transform target;

    private void FixedUpdate()
    {
        // ! - not. We are checking if there is no target.
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walls") // Fix 28.06
        {
            Destroy(gameObject);
        }
        else
        {
            collision.gameObject.GetComponent<EnemyHP>().TakeDamage(bulletDmg);
            Destroy(gameObject);
        }
    }
}
