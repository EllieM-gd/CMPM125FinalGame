using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        // Splash in order to pick up enemy
        if (enemyAI.IsSplashed && PlayerInTrigger && Input.GetButtonDown("Fire2"))
        {
            // Pick up enemy
            if (!enemyAI.IsPickedUp)
            {
                enemyAI.transform.parent = Player.transform;
                enemyAI.transform.position = Player.transform.position + Player.transform.forward.normalized * 1.25f + Vector3.up * 0.5f;
                enemyAI.transform.rotation = Player.transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                enemyAI.EnemyPickedUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure one item picked up at a time
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount == 1)
            {
                PlayerInTrigger = true;
            }
        }
        // Splash enemy if it touches water
        if (other.CompareTag("Water"))
        {
            enemyAI.EnemySplashed();
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
