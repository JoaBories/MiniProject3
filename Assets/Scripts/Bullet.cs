using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private float accel;
    [SerializeField] private float maxSpeed;

    [SerializeField] private GameObject explosionPrefab;

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameCage"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject currentExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(currentExplosion, 1f);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
