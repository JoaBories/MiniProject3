using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garun : MonoBehaviour
{
    Transform player;

    float goalZrotation;
    float zRotationSmoothness;

    private void Awake()
    {
        player = PlayerMovements.instance.transform;
    }

    private void Update()
    {
        
    }
}
