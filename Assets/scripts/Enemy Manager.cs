using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    public static EnemyManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private GameObject Enemy;
    [SerializeField] public int AmmountToPool;
    [SerializeField] public int NumberOfEnemies;
    public List<GameObject> EnemyPool;

    // Start is called before the first frame update
    void Start()
    {
        AmmountToPool = NumberOfEnemies * 2;

        EnemyPool = new List<GameObject>();
        GameObject Temp;
        for (int i = 0; i < AmmountToPool; i++)
        {
            Temp = Instantiate(Enemy);

            EnemyAI enemyAI = Temp.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.Agent = Temp.GetComponent<NavMeshAgent>();
                if (enemyAI.Agent == null)
                {
                    Debug.Log($"NavMeshAgent is missing on instantiated Enemy prefab: {Temp.name}");
                }

                Vector3 spawnLocation = enemyAI.GeneratePointOnMap();
                while (!enemyAI.CheckDestination(spawnLocation))
                {
                    spawnLocation = enemyAI.GeneratePointOnMap();
                }

                Temp.transform.position = spawnLocation;
            }

            Temp.SetActive(false);
            EnemyPool.Add(Temp);
        }

        for (int i  = 0; i < NumberOfEnemies; i++)
        {
            GameObject NewEnemy = GetPooledObject();

            NewEnemy.SetActive(true);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < AmmountToPool; i++)
        {
            if (!EnemyPool[i].activeInHierarchy)
            {
                EnemyAI enemyAI = EnemyPool[i].GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.Agent = EnemyPool[i].GetComponent<NavMeshAgent>();
                }
                return EnemyPool[i];
            }
        }
        return null;
    }

    public IEnumerator SpawnNewEnemy()
    {
        GameObject EnemyToSpawn = Instantiate(Enemy);

        EnemyAI enemyAI = EnemyToSpawn.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.Agent = EnemyToSpawn.GetComponent<NavMeshAgent>();
            if (enemyAI.Agent == null)
            {
                Debug.Log($"NavMeshAgent is missing on instantiated Enemy prefab: {EnemyToSpawn.name}");
            }

            Vector3 spawnLocation = enemyAI.GeneratePointOnMap();
            while (!enemyAI.CheckDestination(spawnLocation))
            {
                spawnLocation = enemyAI.GeneratePointOnMap();
            }

            EnemyToSpawn.transform.position = spawnLocation;
        }

        EnemyToSpawn.SetActive(false);
        EnemyPool.Add(EnemyToSpawn);

        yield return new WaitForSeconds(Random.Range(5, 10));

        EnemyToSpawn.SetActive(true);
    }

    //private void EnemyReset()
    //{
        
    //}
}
