using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garun : MonoBehaviour
{
    Transform player;

    float goalZrotation;
    float zRotationSmoothness;

    private void Start()
    {
        player = PlayerMovements.instance.transform;
    }

    private void Update()
    {
        Vector2 difference = player.transform.position - transform.position;
        goalZrotation = -90 - Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;
    }
}
