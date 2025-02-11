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

    [SerializeField] private float maxRotate;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private float rotateSmoothness;

    private Rigidbody _rb;
    private float goalRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        goalRotation = Mathf.Sign(moveDir.y) * Mathf.LerpAngle(0, maxRotate, Mathf.Abs(moveDir.y));

        float Xrotation = Mathf.LerpAngle(playerObj.transform.rotation.eulerAngles.x, goalRotation, Time.deltaTime * rotateSmoothness);
        playerObj.transform.rotation = Quaternion.Euler(Xrotation, 0, 0);
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
    }
}
