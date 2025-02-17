using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public GameObject reward;

    public int enemyNB;
    public bool finishSpawn;

    public void ChildShot(Vector3 childPos)
    {
        enemyNB--;

        if (enemyNB == 0)
        {
            if (reward != null)
            {
                Instantiate(reward, childPos, Quaternion.identity);
            }
        }

        if (transform.childCount == 0 && finishSpawn)
        {
            Destroy(gameObject);
        }
    }

    public void ChildOut()
    {
        if (transform.childCount == 0 && finishSpawn)
        {
            Destroy(gameObject);
        }
    }
}
