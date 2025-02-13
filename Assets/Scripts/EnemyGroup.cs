using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject reward;

    public void CheckForChildrens(Vector3 lastChildPos)
    {
        if (transform.childCount == 1)
        {
            if (reward != null)
            {
                Instantiate(reward, lastChildPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
