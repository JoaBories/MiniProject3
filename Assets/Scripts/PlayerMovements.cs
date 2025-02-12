using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovements : MonoBehaviour
{
    Vector2 moveDir;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float accel;
    [SerializeField] private float deccel;

    [SerializeField] private float maxRotate;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private float rotateSmoothness;

    [SerializeField] private float minThrusterSize;
    [SerializeField] private float maxThrusterSize;
    [SerializeField] private float thrusterSizeChangeSmoothness;
    
    [SerializeField] private float minThrusterI;
    [SerializeField] private float maxThrusterI;
    [SerializeField] private float thrusterIChangeSmoothness;

    [SerializeField] private VisualEffect thrusterVFX;

    private Rigidbody _rb;
    private float goalRotation;
    private float goalSize;
    private float goalI;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        goalRotation = Mathf.Sign(moveDir.y) * Mathf.LerpAngle(0, maxRotate, Mathf.Abs(moveDir.y));

        float Xrotation = Mathf.LerpAngle(playerObj.transform.rotation.eulerAngles.x, goalRotation, Time.deltaTime * rotateSmoothness);
        playerObj.transform.rotation = Quaternion.Euler(Xrotation, 0, 0);

        goalSize = Mathf.Lerp(minThrusterSize, maxThrusterSize, (moveDir.x + 1)/2);
        float size = Mathf.Lerp(thrusterVFX.GetFloat("size"), goalSize, thrusterSizeChangeSmoothness * Time.deltaTime);
        thrusterVFX.SetFloat("size", size);

        goalI = Mathf.Lerp(minThrusterI, maxThrusterI, (moveDir.x + 1) / 2);
        float intensity = Mathf.Lerp(thrusterVFX.GetFloat("intensity"), goalI, thrusterIChangeSmoothness * Time.deltaTime);
        thrusterVFX.SetFloat("intensity", intensity);
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
