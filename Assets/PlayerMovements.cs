using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    Vector2 moveDir;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float accel;
    [SerializeField] private float deccel;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 targetMovement = moveDir * movementSpeed;
        Vector2 movementDiff = targetMovement - _rb.velocity;
        float accelRate = (Mathf.Abs(targetMovement.sqrMagnitude) > 0.1f) ? accel : deccel;

        Vector2 movement = movementDiff * accelRate;

        _rb.AddForce(movement, ForceMode.Force);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
        Debug.Log("yo");
    }
}
