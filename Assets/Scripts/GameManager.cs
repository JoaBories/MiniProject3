using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float playerGroupSpeed;

    public int Score;

    private GameObject checkpoint;
    private float notMovingTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (notMovingTime <= 0)
        {
            transform.position += playerGroupSpeed * Time.deltaTime * Vector3.right;
        }
        else
        {
            notMovingTime -= Time.deltaTime;
        }

    }

    public void AddScore(int score)
    {
        Score += score;
        HUD.instance.UpdateScore(Score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            checkpoint = other.gameObject;
        }
    }

    public void Die()
    {
        if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position - new Vector3(40 , 0, 0);
            notMovingTime = 2;
        }
    }
}
