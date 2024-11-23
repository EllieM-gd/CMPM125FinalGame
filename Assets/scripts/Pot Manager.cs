using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotManager : MonoBehaviour
{

    private static PotManager _instance;

    public static PotManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] public GameObject Pot;
    public bool IsPickedUp = false;
    public bool CanThrow = true;
    public Vector3 PotBasePosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        PotBasePosition = Pot.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
