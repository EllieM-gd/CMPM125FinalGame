using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private EnemyAI enemyAI;
    private PotManager PotManager;
    private PlayerManager PlayerManager;
    private GameObject Player;
    private bool PlayerInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        PotManager = PotManager.Instance;
        PlayerManager = PlayerManager.Instance;
        Player = PlayerManager.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.IsSplashed && PlayerInTrigger && Input.GetButtonDown("Fire2"))
        {
            if (!enemyAI.IsPickedUp)
            {
                enemyAI.transform.parent = Player.transform;
                enemyAI.transform.position = Player.transform.position + Player.transform.forward.normalized;
                enemyAI.EnemyPickedUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount == 0)
            {
                PlayerInTrigger = true;
            }
        }
        // Check if Player is holding pot and that pot has water
        if (other.transform.childCount > 0)
        {
            if (other.transform.GetChild(0) != null)
            {
                if (other.transform.GetChild(0).GetChild(0) != null)
                {
                    if (other.transform.GetChild(0).GetChild(0).CompareTag("Water") && other.transform.GetChild(0).GetChild(0).gameObject.activeSelf)
                    {
                        // Splash enemy and disable their movement
                        other.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        enemyAI.EnemySplashed();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
        }
    }
}
