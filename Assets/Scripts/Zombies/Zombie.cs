using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    //Calculation Variables
    [Header("Zombie Behaviour Stats")]

    [SerializeField] private Transform target;
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float stopDistance = 15f;
    [SerializeField] float walkDistance = 14f;
    [SerializeField] float runDistance = 6f;
    [SerializeField] bool canFollow = true;


    //Custom classes Referece
    private readonly NumberOfZombies _numberOfZombies = new NumberOfZombies();

    //Reference variables
    private float _distanceFromTarget = Mathf.Infinity;

    // Component variables
    NavMeshAgent _navMeshAgent;
    Rigidbody _zombieRigidbody;
    bool _isProvoked = false;

    private void Start()
    {
        _zombieRigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        target =  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnEnable() => _numberOfZombies.OnEnabled();

    private void OnDisable() => _numberOfZombies.OnDisabled();

    void Update()
    {
        if (canFollow) ProcessZombieBehaviour();
        else _navMeshAgent.isStopped = true;
    }

    void ProcessZombieBehaviour()
    {
        _distanceFromTarget = Vector3.Distance(target.position, transform.position);

        if(_isProvoked)
        {
            EngageTarget();
        }
        // Within walk and run range --> engage target
        if (_distanceFromTarget <= stopDistance)
        {
            _isProvoked = true;
        }
        // Too far from target --> stop moving
        if (_distanceFromTarget > stopDistance)
        {
            _isProvoked = false;
            _navMeshAgent.isStopped = true;
        }
    }

    void EngageTarget()
    {
        // within enemy range but not so near
        if (_distanceFromTarget > runDistance && _distanceFromTarget <= walkDistance)
            ChaseTarget(target, walkSpeed);
        else if (_distanceFromTarget <= runDistance) ChaseTarget(target, runSpeed);
    }


    private void ChaseTarget(Transform target, float speed)
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(target.position - (transform.forward));
    }
}


/// <summary>
/// Thulk Code
/// </summary>
public class NumberOfZombies //Handles the number of enemies on scene. 
{
    public readonly static List<NumberOfZombies> Enemies = new List<NumberOfZombies>();
    public static int EnemiesInScene => Enemies.Count;
    public void OnEnabled() => Enemies.Add(this);
    public void OnDisabled() => Enemies.Remove(this);

    public int GetEnemiesInScene() => EnemiesInScene;
}