using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    [SerializeField] float shootCooldown;
    [SerializeField] float spamCooldownReduction;
    [SerializeField] float initialBulletSpeed;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] private float twoBulletDistance;

    private float lastShoot;

    private bool shooting;

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (PlayerUpgrade.Instance.activeUpgrades.Contains(RewardTypes.FireRate))
        {
            lastShoot -= Time.fixedDeltaTime;
        }

        lastShoot -= Time.fixedDeltaTime;

        if (shooting && lastShoot < -shootCooldown)
        {
            Shoot(bulletPrefab, initialBulletSpeed);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            shooting = true;
            if(lastShoot < -shootCooldown + spamCooldownReduction)
            {
                Shoot(bulletPrefab, initialBulletSpeed);
            }
        }
        else if (context.canceled)
        {
            shooting = false;
        }
    }

    public void Shoot(GameObject prefab, float bulletSpeed)
    {
        if (PlayerUpgrade.Instance.activeUpgrades.Contains(RewardTypes.AdditionalGun))
        {
            GameObject currentbullet = Instantiate(prefab, bulletSpawn.position + new Vector3(0f, twoBulletDistance / 2, 0f), Quaternion.identity);
            currentbullet.GetComponent<Rigidbody>().AddForce(transform.right.normalized * bulletSpeed, ForceMode.Impulse);
            currentbullet = Instantiate(prefab, bulletSpawn.position - new Vector3(0f, twoBulletDistance / 2, 0f), Quaternion.identity);
            currentbullet.GetComponent<Rigidbody>().AddForce(transform.right.normalized * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            GameObject currentbullet = Instantiate(prefab, bulletSpawn.position, Quaternion.identity);
            currentbullet.GetComponent<Rigidbody>().AddForce(transform.right.normalized * bulletSpeed, ForceMode.Impulse);
        }
        lastShoot = 0;
    }
}
