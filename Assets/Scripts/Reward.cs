using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameCage"))
        {
            Destroy(gameObject);
        }
    }
}
