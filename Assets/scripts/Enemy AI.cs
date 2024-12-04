using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    // This code was origially created by Hunter Kingsley for the Module 1 projects, it has been modified to work here

    [SerializeField] private GameObject player;
    private Vector3 PlayerPosition;
    public NavMeshAgent Agent;
    private MeshRenderer meshRenderer;
    public GameObject plate;
    [SerializeField] private LayerMask layerMask;
    private Vector3 Destination = Vector3.zero;
    [SerializeField] private float FleeRange = 4f;
    public bool IsSplashed = false;
    public bool IsPickedUp = false;
    public bool InDashState = false;
    private float AgentBaseSpeed = 0f;
    private float AgentBaseAcceleration = 0f;
    private float AgentBaseAngluarSpeed = 0f;
    [SerializeField] public GameObject eyeGameObjects;

    //Below Block will need to be uncommented when pooling is implemented

    //private void OnEnable()
    //{
    //    if (Agent != null)
    //    {
    //        Agent.SetDestination(Destination);
    //    }
    //}

    private void Awake()
    {
        // Assign the NavMeshAgent component when the script initializes so everything doesn't die
        Agent = GetComponent<NavMeshAgent>();

        if (Agent == null)
        {
            Debug.LogError($"NavMeshAgent component is missing on {gameObject.name}. GO ATTACH IT.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        meshRenderer = GetComponent<MeshRenderer>();

        plate = gameObject.transform.Find("Plate").gameObject;

        if (plate != null)
        {
            plate.SetActive(false);
        }

        PlayerPosition = player.transform.position;

        Agent = GetComponent<NavMeshAgent>();

        Destination = this.gameObject.GetComponent<Transform>().position;

        //Checks to make sure the generated position is valid, and when it is, the agent is sent there
        while (!CheckDestination(Destination))
        {
            Destination = GeneratePointOnMap();
        }

        Agent.SetDestination(Destination);

        AgentBaseAcceleration = Agent.acceleration;
        AgentBaseSpeed = Agent.speed;
        AgentBaseAngluarSpeed = Agent.angularSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Random.Range(1, 1001) == 1) && (InDashState == false))
        {
            InDashState = true;
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
        }

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
    public bool CheckDestination(Vector3 tempDestination)
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
        Agent.enabled = false;
        meshRenderer.enabled = false;
        eyeGameObjects.SetActive(false);
        plate.SetActive(true);
    }

    public void EnemyPickedUp()
    {
        IsPickedUp = true;
        Agent.enabled = false;
    }

    private IEnumerator Dash()
    {
        Agent.speed = 50;
        Agent.acceleration = 50;
        Agent.angularSpeed = 50;
        yield return new WaitForSeconds(0.75f);
        Agent.speed = AgentBaseSpeed;
        Agent.acceleration = AgentBaseAcceleration;
        Agent.angularSpeed = AgentBaseAngluarSpeed;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(Random.Range(4f, 7f));
        InDashState = false;
    }

    //private float EaseOutQuart(float t)
    //{
    //    return 1 - Mathf.Pow(1 - t, 4);
    //}
}
