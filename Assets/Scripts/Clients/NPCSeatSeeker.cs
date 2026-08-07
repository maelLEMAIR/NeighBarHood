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

    public Transform playerTransform;

    public enum SeekState
    {
        Wandering, 
        WalkingToChair, 
        Sitting, 
        WaitingOrderToBeTaken, 
        WaitingHisOrder, 
        EvaluateHisOrder, 
        Leaving
    }
    private SeekState state = SeekState.Wandering;

    [Header("Balade")]
    private int wanderAreaMask;
    private Transform currentWanderPoint;

    [Header("Temps avant de vouloir s'asseoir")]
    public float minTimeBeforeSitting = 8f;
    public float maxTimeBeforeSitting = 20f;
    private float sitDesireTimer;
    private bool wantsToSit = false;

    [Header("Temps assis")]
    public float minSitTime = 5f;
    public float maxSitTime = 20f;
    private float sitTimer;
    
    [Header("Order")] 
    public GameObject thinkSprite;
    
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
                ChooseHisOrder();
                break;

            case SeekState.WaitingOrderToBeTaken:
                if ( thinkSprite.activeSelf )
                {
                    thinkSprite.transform.LookAt(playerTransform);
                }
                break;
            
            case SeekState.WaitingHisOrder:
                break;
            
            case SeekState.EvaluateHisOrder:
                break;
            
            case SeekState.Leaving:
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

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            PickNewWanderPoint();
        }
    }

    public void PickNewWanderPoint()
    {
        if (WanderPointManager.Instance == null)
        {
            Debug.LogWarning("WanderPointManager introuvable !");
            return;
        }
        
        Transform point = WanderPointManager.Instance.GetRandomPointExcluding(currentWanderPoint);

        if (point != null)
        {
            currentWanderPoint = point;

            if (NavMesh.SamplePosition(point.position, out NavMeshHit hit, 1f, wanderAreaMask))
                agent.SetDestination(hit.position);
            
        }
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
        agent.enabled = false;
        
        Vector3 seatPos = new Vector3(
            targetChair.seatPoint.position.x,
            targetChair.seatPoint.position.y + targetChair.seatPoint.localScale.y + transform.localScale.y,
            targetChair.seatPoint.position.z
        );

        transform.position = seatPos; 
        transform.rotation = targetChair.seatPoint.rotation;
        targetChair.Occupy(gameObject);
        
        obstacle.enabled = true;

        sitTimer = Random.Range(minSitTime, maxSitTime);
        state = SeekState.Sitting;
    }

    void ChooseHisOrder()
    {
        sitTimer -=  Time.deltaTime;
        if (sitTimer <= 0f)
        {
            // Prendre la commande dans l'itemManager (un ou plusieurs item)
            state = SeekState.WaitingOrderToBeTaken;
            thinkSprite.SetActive(true);
        }
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