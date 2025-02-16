using UnityEngine;

public class Garun : MonoBehaviour
{
    Transform player;

    float goalZrotation;
    [SerializeField] private float zRotationSmoothness;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float randomShootInterval;
    [SerializeField, Tooltip("More it is less shoot there will be")] private int shootProba;
    private float lastTryShoot;

    private void Start()
    {
        player = PlayerMovements.instance.transform;
    }

    private void Update()
    {
        Vector2 difference = player.transform.position - transform.position;
        goalZrotation = -90 - Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(transform.rotation.eulerAngles.z, goalZrotation, zRotationSmoothness * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        lastTryShoot -= Time.fixedDeltaTime;
        if (lastTryShoot <= 0)
        {
            lastTryShoot = randomShootInterval;
            if (Random.Range(0, shootProba) == 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.parent = transform.parent.parent;
        bullet.GetComponent<Rigidbody>().velocity = -transform.right * bulletSpeed;
    }
}
