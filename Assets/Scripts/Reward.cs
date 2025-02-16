using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public enum RewardTypes
{
    Shield,
    AdditionalGun
}

[Serializable]
public struct OrbColor
{
    [ColorUsageAttribute(true, true)]
    public Color FrontColor;

    [ColorUsageAttribute(true, true)]
    public Color BackColor;

    [ColorUsageAttribute(true, true)]
    public Color InnerColor;
}


public class Reward : MonoBehaviour
{
    [SerializeField] private List<OrbColor> orbColor;
    [SerializeField] private VisualEffect orbVFX;

    [Header("Test to unserialize")]
    public RewardTypes rewardType;
    [SerializeField] private bool random;



    private void Awake()
    {
        bool destroy = true;
        Collider[] colideList = Physics.OverlapBox(transform.position, new Vector3(4, 3, 2));
        foreach (Collider c in colideList)
        {
            if (c.CompareTag("GameCage"))
            {
                destroy = false;
            }
        }

        if (destroy)
        {
            Destroy(gameObject);
        }

        if (random)
        {
            rewardType = (RewardTypes)UnityEngine.Random.Range(0, Enum.GetNames(typeof(RewardTypes)).Length);
        }

        orbVFX.SetVector4("FrontColor", orbColor[(int)rewardType].FrontColor);
        orbVFX.SetVector4("BackColor", orbColor[(int)rewardType].BackColor);
        orbVFX.SetVector4("InnerColor", orbColor[(int)rewardType].InnerColor);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameCage"))
        {
            Destroy(gameObject);
        }
    }
}
