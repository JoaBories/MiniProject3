using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Spawn(int number, float interval, float speed, GameObject enemy, AnimationCurve path)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject currentEnemy = Instantiate(enemy, transform);
            currentEnemy.GetComponent<AIPath>().Initialize(path, speed);
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGroup"))
        {
            EnemyGroup enemyGroup = other.GetComponent<EnemyGroup>();
            int gNumber = enemyGroup.number;
            float gInterval = enemyGroup.interval;
            float gSpeed = enemyGroup.speed;
            GameObject gEnemy = enemyGroup.prefab;
            AnimationCurve gPath = enemyGroup.path;

            StartCoroutine(Spawn(gNumber, gInterval, gSpeed, gEnemy, gPath));
        }
    }
}
