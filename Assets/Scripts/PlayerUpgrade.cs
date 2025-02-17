using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUpgrade : MonoBehaviour
{
    public static PlayerUpgrade Instance;

    private Animator _animator;

    [SerializeField] private List<RewardTypes> upgrades;
    [SerializeField] private int maxUpgradeAmount;

    public List<RewardTypes> activeUpgrades;

    [SerializeField] private float shieldDuration;
    [SerializeField] private float additionalGunDuration;
    [SerializeField] private float fireRateDuration;

    private float shieldTimer;
    private float additionalGunTimer;
    private float fireRateTimer;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        HUD.instance.UpdateUpgrades(upgrades);
    }

    private void FixedUpdate()
    {
        shieldTimer -= Time.fixedDeltaTime;
        additionalGunTimer -= Time.fixedDeltaTime;
        fireRateTimer -= Time.fixedDeltaTime;

        if (shieldTimer <= 0)
        {
            activeUpgrades.Remove(RewardTypes.Shield);
            _animator.Play("shieldOff");
        }

        if (additionalGunTimer <= 0)
        {
            activeUpgrades.Remove(RewardTypes.AdditionalGun);
        }

        if (fireRateTimer <= 0)
        {
            activeUpgrades.Remove(RewardTypes.FireRate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerCapsule"))
        {
            if (upgrades.Count < maxUpgradeAmount)
            {
                upgrades.Add(other.GetComponent<Reward>().rewardType);
                HUD.instance.UpdateUpgrades(upgrades);
            }
            GameManager.instance.AddScore(100);
            Destroy(other.gameObject);
        }
    }

    public void UseUpgrade(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (upgrades.Count > 0)
            {
                int index = 0;
                RewardTypes upgrade = upgrades[index];
                while (activeUpgrades.Contains(upgrade) && index <= upgrades.Count)
                {
                    index++;
                    if (index >= upgrades.Count) return;

                    upgrade = upgrades[index];
                }

                upgrades.RemoveAt(index);

                HUD.instance.UpdateUpgrades(upgrades);

                switch (upgrade)
                {
                    case RewardTypes.Shield:
                        activeUpgrades.Add(RewardTypes.Shield);
                        shieldTimer = shieldDuration;
                        _animator.Play("shieldOn");
                        break;

                    case RewardTypes.AdditionalGun:
                        activeUpgrades.Add(RewardTypes.AdditionalGun);
                        additionalGunTimer = additionalGunDuration;
                        break;

                    case RewardTypes.FireRate:
                        activeUpgrades.Add(RewardTypes.FireRate);
                        fireRateTimer = fireRateDuration;
                        break;
                }
            }
        }
    }
}
