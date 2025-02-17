using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float playerGroupSpeed;

    public int Score;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        transform.position += playerGroupSpeed * Time.deltaTime * Vector3.right;
    }

    public void AddScore(int score)
    {
        Score += score;
        HUD.instance.UpdateScore(Score);
    }
}
