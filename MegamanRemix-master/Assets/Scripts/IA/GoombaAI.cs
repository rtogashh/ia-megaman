using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAI : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D rdb;
    public float visionDistance = 10f;
    public LayerMask visionMask; // Defina no Inspector quais layers bloqueiam a visão

    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (target && CanSeeTarget())
        {
            Vector3 dif = target.transform.position - transform.position;
            rdb.AddForce(dif);
        }
    }

    private bool CanSeeTarget()
    {
        if (target == null) return false;

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > visionDistance)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, visionMask);
        if (hit.collider != null && hit.collider.gameObject == target)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
}