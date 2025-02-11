using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPath : MonoBehaviour
{
    [SerializeField] private AnimationCurve path;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.2f;
        transform.localPosition = new Vector3(0, path.Evaluate(time) * 10, 0);
        if (time > 1)
        {
            time -= 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        
    }
}
