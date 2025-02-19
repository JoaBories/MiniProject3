using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private AnimationCurve path;
    private float speed;
    private EnemyGroup enemyGroup;
    private Vector2 pathScale = new Vector2(75, 25);

    float time = 1;

    void Update()
    {
        time -= Time.deltaTime * speed;

        Vector2 nextPos = new(time * pathScale.x, path.Evaluate(time) * pathScale.y);

        transform.localPosition = new Vector2(nextPos.x - pathScale.x, nextPos.y);
    }

    public void Initialize(AnimationCurve iPath, float iSpeed, EnemyGroup iEnemyGroup)
    {
        path = iPath;
        speed = iSpeed;
        enemyGroup = iEnemyGroup;
    }

    public void Die()
    {
        SoundManager.instance.PlaySound("enemyDie", transform.position);
        enemyGroup.ChildShot(transform.position);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameCage"))
        {
            enemyGroup.ChildOut();
            Destroy(gameObject);
        }
    }
}
