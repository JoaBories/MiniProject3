using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private GameObject enemyGroupPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Spawn(int number, float interval, float speed, GameObject enemy, AnimationCurve path, EnemyGroup currentEnemyGroup)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject currentEnemy = Instantiate(enemy, currentEnemyGroup.transform);
            currentEnemy.GetComponent<EnemyBehaviour>().Initialize(path, speed, currentEnemyGroup);
            yield return new WaitForSeconds(interval);
        }
        currentEnemyGroup.finishSpawn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyGroup"))
        {
            EnemyGroupToSpawn enemyGroupToSpawn = other.GetComponent<EnemyGroupToSpawn>();
            int gNumber = enemyGroupToSpawn.number;
            float gInterval = enemyGroupToSpawn.interval;
            float gSpeed = enemyGroupToSpawn.speed;
            GameObject gEnemy = enemyGroupToSpawn.prefab;
            AnimationCurve gPath = enemyGroupToSpawn.path;

            EnemyGroup gCurrentEnemyGroup = Instantiate(enemyGroupPrefab, transform).GetComponent<EnemyGroup>();
            gCurrentEnemyGroup.reward = enemyGroupToSpawn.reward;
            gCurrentEnemyGroup.enemyNB = enemyGroupToSpawn.number;
            StartCoroutine(Spawn(gNumber, gInterval, gSpeed, gEnemy, gPath, gCurrentEnemyGroup));
        }
    }
}
