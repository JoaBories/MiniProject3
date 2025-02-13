using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float playerGroupSpeed;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position += playerGroupSpeed * Time.deltaTime * Vector3.right;
    }
}
