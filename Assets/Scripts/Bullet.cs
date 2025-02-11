using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float accel;
    [SerializeField] private float maxSpeed;

    private float lifetime;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;

        if (rb.velocity.sqrMagnitude < maxSpeed * maxSpeed)
        {
            rb.AddForce(rb.velocity.normalized * accel, ForceMode.Force);
        }

        if (lifetime < -lifeSpan)
        {
            Destroy(gameObject);
        }

        Debug.Log(Mathf.Sqrt(maxSpeed * maxSpeed * 10000));
    }
}
