using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameCage"))
        {
            Destroy(gameObject);
        }
    }
}
