using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPath : MonoBehaviour
{
    [SerializeField] private AnimationCurve path;
    [SerializeField] private float speed;
    float time = 1;
    [SerializeField] private Vector2 pathScale = new Vector2(10, 5);



    void Update()
    {
        time -= Time.deltaTime * speed;

        Vector2 nextPos = new Vector2(time * pathScale.x, path.Evaluate(time) * pathScale.y);

        transform.localPosition = nextPos;


        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
