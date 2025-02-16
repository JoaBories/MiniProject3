using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private List<RewardTypes> upgrades;
    [SerializeField] private int maxUpgradeAmount;

    [SerializeField] private List<RewardTypes> activeUpgrades;

    [SerializeField] private float shieldDuration;
    [SerializeField] private float additionalGunDuration;

    private float shieldTimer;
    private float additionalGunTimer;

    private void FixedUpdate()
    {
        shieldTimer -= Time.fixedDeltaTime;
        additionalGunTimer -= Time.fixedDeltaTime;

        if (shieldTimer <= 0)
        {
            activeUpgrades.Remove(RewardTypes.Shield);
        }

        if (additionalGunTimer <= 0)
        {
            activeUpgrades.Remove(RewardTypes.AdditionalGun);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerCapsule"))
        {
            if (upgrades.Count < maxUpgradeAmount)
            {
                upgrades.Add(other.GetComponent<Reward>().rewardType);
            }
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


                switch (upgrade)
                {
                    case RewardTypes.Shield:
                        activeUpgrades.Add(RewardTypes.Shield);
                        shieldTimer = shieldDuration;
                        break;

                    case RewardTypes.AdditionalGun:
                        activeUpgrades.Add(RewardTypes.AdditionalGun);
                        additionalGunTimer = additionalGunDuration;
                        break;
                }
            }
        }
    }
}
