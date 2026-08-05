using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavMeshObstacle))]
public class NPCSeatSeeker : MonoBehaviour
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private ChairSlot targetChair;
    private Transform exitTarget;

    public  enum SeekState { Wandering, WalkingToChair, Sitting, Leaving }
    private SeekState state = SeekState.Wandering;

    [Header("Balade")]
    public float wanderRadius = 10f;
    public float wanderInterval = 4f;
    private float wanderTimer;
    private int wanderAreaMask;

    [Header("Temps avant de vouloir s'asseoir")]
    public float minTimeBeforeSitting = 8f;
    public float maxTimeBeforeSitting = 20f;
    private float sitDesireTimer;
    private bool wantsToSit = false;

    [Header("Temps assis")]
    public float minSitTime = 5f;
    public float maxSitTime = 20f;
    private float sitTimer;

    public SeekState GetState() { return state; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.carving = true;
        obstacle.enabled = false;
    }

    void Start()
    {
        agent.avoidancePriority = Random.Range(0, 99);

        int noWanderMask = 1 << NavMesh.GetAreaFromName("NoWander");
        wanderAreaMask = NavMesh.AllAreas & ~noWanderMask;
        agent.areaMask = wanderAreaMask;

        sitDesireTimer = Random.Range(minTimeBeforeSitting, maxTimeBeforeSitting);
        PickNewWanderPoint();
    }

    void Update()
    {
        switch (state)
        {
            case SeekState.Wandering:
                UpdateWander();
                break;

            case SeekState.WalkingToChair:
                if (targetChair != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    SitDown();
                }
                break;

            case SeekState.Sitting:
                sitTimer -= Time.deltaTime;
                if (sitTimer <= 0f)
                {
                    StandUp();
                }
                break;

            case SeekState.Leaving:
                // Rien à faire ici : c'est ExitZone (trigger) qui appelle Despawn()
                // quand le PNJ atteint physiquement la zone de sortie.
                break;
        }
    }

    // ---------- WANDER ----------

    void UpdateWander()
    {
        if (!wantsToSit)
        {
            sitDesireTimer -= Time.deltaTime;
            if (sitDesireTimer <= 0f)
            {
                wantsToSit = true;
                TryFindChair();
                return;
            }
        }

        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f || (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance))
        {
            PickNewWanderPoint();
        }
    }

    public void PickNewWanderPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRadius;
        randomPoint.y = transform.position.y;
        
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, wanderRadius, wanderAreaMask))
        {
            agent.SetDestination(hit.position);
            state = SeekState.Wandering;
        }

        wanderTimer = wanderInterval;
    }

    // ---------- CHAISE ----------

    void TryFindChair()
    {
        targetChair = ChairManager.Instance.RequestChair(gameObject);
        if (targetChair != null)
        {
            agent.areaMask = NavMesh.AllAreas;
            agent.SetDestination(targetChair.seatPoint.position);
            state = SeekState.WalkingToChair;
        }
        else
        {
            agent.areaMask = wanderAreaMask;
            state = SeekState.Wandering;
            Invoke(nameof(TryFindChair), 3f);
        }
    }

    void SitDown()
    {
        agent.isStopped = true;
        transform.position.Set(
            targetChair.seatPoint.position.x, 
            targetChair.seatPoint.localScale.y / 2.0f + transform.localScale.y / 2.0f, 
            targetChair.seatPoint.position.z);
        
        transform.rotation = targetChair.seatPoint.rotation;
        targetChair.Occupy(gameObject);

        // le PNJ devient un obstacle une fois assis
        agent.enabled = false;      // NavMeshAgent et NavMeshObstacle ne peuvent pas être actifs en même temps
        obstacle.enabled = true;

        sitTimer = Random.Range(minSitTime, maxSitTime);
        state = SeekState.Sitting;
    }

    public void StandUp()
    {
        if (targetChair != null)
        {
            targetChair.Release();
            targetChair = null;
        }

        obstacle.enabled = false;
        agent.enabled = true;
        agent.isStopped = false;

        GoToExit();
    }

    void GoToExit()
    {
        exitTarget = ExitManager.Instance.GetNearestExit(transform.position);

        if (exitTarget != null)
        {
            agent.areaMask = NavMesh.AllAreas;
            agent.SetDestination(exitTarget.position);
            state = SeekState.Leaving;
        }
        else
        {
            PickNewWanderPoint();
        }
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}