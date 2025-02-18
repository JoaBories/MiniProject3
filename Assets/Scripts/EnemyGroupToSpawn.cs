using UnityEngine;

public class EnemyGroupToSpawn : MonoBehaviour
{
    public AnimationCurve path;
    public int number;
    public GameObject prefab;
    public float interval;
    public float speed;
    public GameObject reward;

    [Header("Gizmos")]
    [SerializeField] private float gizmosPrecision = 10;
    [SerializeField] private Vector2 gizmosScale = new(10, 5);
    [SerializeField] private Transform origin;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (origin == null) origin = transform;

        Vector2 lastpoint = origin.position;
        lastpoint.y = origin.position.y + (path.Evaluate(1) * gizmosScale.y);

        for (int i = (int) gizmosPrecision; i >= 0; i--)
        {
            Vector2 point = new(origin.position.x + (i / gizmosPrecision * gizmosScale.x) - gizmosScale.x, origin.position.y + (path.Evaluate(i / gizmosPrecision) * gizmosScale.y));
            Gizmos.DrawLine(lastpoint, point);
            lastpoint = point;
        }
    }
}
