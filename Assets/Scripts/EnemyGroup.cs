using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] public AnimationCurve path;
    [SerializeField] private float gizmosPrecision = 10;
    [SerializeField] private Vector2 gizmosScale = new Vector2(10, 5);

    [SerializeField] private Transform origin;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (origin == null) origin = transform;

        Vector2 lastpoint = origin.position;
        lastpoint.y = origin.position.y + (path.Evaluate(1) * gizmosScale.y);

        for (int i = (int) gizmosPrecision; i >= 0; i--)
        {
            Vector2 point = new Vector2(origin.position.x + (i / gizmosPrecision * gizmosScale.x) - gizmosScale.x, origin.position.y + (path.Evaluate(i / gizmosPrecision) * gizmosScale.y));
            Gizmos.DrawLine(lastpoint, point);
            lastpoint = point;
        }
    }
}
