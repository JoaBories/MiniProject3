using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayerLife : MonoBehaviour
{
    private int life = 3;
    private PlayerInput input;

    [SerializeField] private GameObject explosionForSpaceShip;
    [SerializeField] private GameObject playerObj;

    private float anotherTimer;
    private bool anotherBool;

    private Collider playerCollider;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        HUD.instance.UpdateLife(life);
    }

    private void Update()
    {
        anotherTimer -= Time.deltaTime;
        if (anotherTimer <= 0 && anotherBool)
        {
            life--;
            HUD.instance.UpdateLife(life);
            if (life <= 0)
            {
                KeepInfo.sceneToRestartIndex = SceneManager.GetActiveScene().buildIndex;
                KeepInfo.score = GameManager.instance.Score;
                SceneManager.LoadScene("Restart");
            }

            GameManager.instance.Die();
            Restart();
            playerObj.SetActive(true);
            playerCollider.enabled = true;
            UnlockMovement();
            anotherBool = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            if (PlayerUpgrade.Instance.activeUpgrades.Contains(RewardTypes.Shield))
            {
                PlayerUpgrade.Instance.BreakShield();
            }
            else
            {
                lockMovement();
                playerObj.SetActive(false);
                anotherTimer = 1;
                playerCollider.enabled = false;
                anotherBool = true;
                Destroy(Instantiate(explosionForSpaceShip, transform.position, Quaternion.identity), 1f);
            }

            Destroy(other.gameObject);
        }
    }

    private void Restart()
    {
        transform.localPosition = new Vector3(-18, 0, 0);

        PlayerUpgrade.Instance.Restart();

        List<GameObject> objectToDestroy = GameObject.FindGameObjectsWithTag("EnemyGroup").ToList();
        objectToDestroy.AddRange(GameObject.FindGameObjectsWithTag("PowerCapsule").ToList());
        objectToDestroy.AddRange(GameObject.FindGameObjectsWithTag("EnemyBullet").ToList());

        foreach (GameObject obj in objectToDestroy)
        {
            Destroy(obj.gameObject);
        }
    }

    public void UnlockMovement()
    {
        input.enabled = true;
    }

    public void lockMovement()
    {
        input.enabled = false;
    }
}
