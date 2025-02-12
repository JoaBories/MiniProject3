using UnityEngine;

public class AIPath : MonoBehaviour
{
    [SerializeField] private AnimationCurve path;
    
    [SerializeField] private float speed;
    [SerializeField] private Vector2 pathScale = new(10, 5);

    float time = 1;

    void Update()
    {
        time -= Time.deltaTime * speed;

        Vector2 nextPos = new(time * pathScale.x, path.Evaluate(time) * pathScale.y);

        transform.localPosition = new Vector2(nextPos.x - pathScale.x, nextPos.y);

        if (time <= -1)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(AnimationCurve iPath, float iSpeed)
    {
        path = iPath;
        speed = iSpeed;
    }
}
