using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // This code was origially created by Hunter Kingsley for the Module 1 projects, it has been modified to work here

    [SerializeField] private GameObject player;
    private Vector3 PlayerPosition;
    public NavMeshAgent Agent;
    [SerializeField] private LayerMask layerMask;
    private Vector3 Destination = Vector3.zero;
    [SerializeField] private float FleeRange = 4f;
    public bool IsSplashed = false;
    public bool IsPickedUp = false;

    //Below Block will need to be uncommented when pooling is implemented

    //private void OnEnable()
    //{
    //    if (Agent != null)
    //    {
    //        Agent.SetDestination(Destination);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        PlayerPosition = player.transform.position;

        Agent = GetComponent<NavMeshAgent>();

        Destination = this.gameObject.GetComponent<Transform>().position;

        //Checks to make sure the generated position is valid, and when it is, the agent is sent there
        while (!CheckDestination(Destination))
        {
            Destination = GeneratePointOnMap();
        }

        Agent.SetDestination(Destination);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSplashed)
        {
            PlayerPosition = player.transform.position;

            float DistanceToPlayer = Vector3.Distance(PlayerPosition, this.gameObject.transform.position);

            if (DistanceToPlayer < FleeRange)
            {
                Agent.ResetPath();

                Vector3 fleeDirection = (this.gameObject.transform.position - PlayerPosition).normalized;
                Vector3 newDestination = this.gameObject.transform.position + fleeDirection * FleeRange;

                while (!CheckDestination(newDestination))
                {
                    newDestination = GeneratePointOnMap();
                }

                Agent.SetDestination(newDestination);
            }
            else if ((this.gameObject.transform.position.x == Destination.x) && (this.gameObject.transform.position.z == Destination.z))
            {
                Destination = GeneratePointOnMap();

                while (!CheckDestination(Destination))
                {
                    Destination = GeneratePointOnMap();
                }

                Agent.SetDestination(Destination);
            }
        } 
    }
    
    //Generates a Point within a 14 x 14 box using perlin noise and returns it as a vector3
    public Vector3 GeneratePointOnMap()
    {
        float xPos = Mathf.PerlinNoise(Time.time + Random.Range(1, 100) * 0.1f, 0) * 14 * (Random.value < 0.5 ? -1 : 1);
        float yPos = 0.8f;
        float zPos = Mathf.PerlinNoise(0, Time.time + Random.Range(1, 100) * 0.1f) * 14 * (Random.value < 0.5 ? -1 : 1);

        Vector3 NewPos = new Vector3(xPos, yPos, zPos);

        return NewPos;
    }

    //Checks to make sure the given destination is one that can be reached by the NavMeshAgent
    private bool CheckDestination(Vector3 tempDestination)
    {
        NavMeshPath path = new NavMeshPath();

        NavMeshHit hit;
        if (NavMesh.SamplePosition(tempDestination, out hit, 20, NavMesh.AllAreas))
        {
            Destination = hit.position;
        }

        Agent.CalculatePath(Destination, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        return false;
    }

    public void EnemySplashed()
    {
        IsSplashed = true;
    }

    public void EnemyPickedUp()
    {
        IsPickedUp = true;
        Agent.enabled = false;
    }
}
