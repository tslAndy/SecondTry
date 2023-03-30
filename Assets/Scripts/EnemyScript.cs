using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyScript : MonoBehaviour
{
    public static event Action PlayerDied;

    [HideInInspector]
    public static bool CanEnemiesSeePlayer { get; set; } = true;

    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();
    private int targetIndex = 0;

    [SerializeField]
    private float playerPosForgetTimer;
    private float playerPosForgetTimerCounter;

    private bool isEnenySeeingPlayer;

    private Animator animator;

    private Transform playerRef;
    private FieldOfView fovRef;

    private AIPath pathScript;
    private AIDestinationSetter destinationScript;
    private EnemyStates currentState = EnemyStates.WalkingBetweenPoints;
    private enum EnemyStates
    {
        Staying,
        WalkingBetweenPoints,
        FollowingPlayer,
        Attacking
    }
    void Start()
    {
        pathScript = GetComponent<AIPath>();
        destinationScript = GetComponent<AIDestinationSetter>();
        animator = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
        fovRef = GetComponent<FieldOfView>();
        playerPosForgetTimerCounter = playerPosForgetTimer;

        Player.PlayerEnteredInvicibility += OnPlayerInvincibleEnter;
        Player.PlayerExitedInvicibility += OnPlayerInvincibleExit;
        destinationScript.target = pathPoints[targetIndex];

        StartCoroutine(CanMoveNext());
    }

    // Update is called once per frame
    void Update()
    {
        if (pathPoints.Count == 1 && currentState != EnemyStates.FollowingPlayer )
        {
            SwitchState(EnemyStates.Staying);
        }
        isEnenySeeingPlayer = fovRef.CanSeePlayer;
        switch (currentState)
        {
            case EnemyStates.Staying:
                animator?.SetTrigger("Staying");
                if (isEnenySeeingPlayer)
                {
                    ChangeTargetToPlayer();
                }
                break;
            case EnemyStates.WalkingBetweenPoints:
                if (isEnenySeeingPlayer)
                {                  
                    ChangeTargetToPlayer();
                }
                break;
            case EnemyStates.FollowingPlayer:
                if (!isEnenySeeingPlayer)
                {
                    playerPosForgetTimerCounter -= Time.deltaTime;
                    if (playerPosForgetTimerCounter <= 0)
                    {
                        ChangeTargetToDefault();
                    }
                }
                break;
            case EnemyStates.Attacking:
                break;
            default:
                break;
        }
    }


    private void SwitchState(EnemyStates newState)
    {
        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            PlayerDied?.Invoke();
        }
    }

    private void OnPlayerInvincibleEnter()
    {
        ChangeTargetToDefault();
        CanEnemiesSeePlayer = false;
    }
    private void OnPlayerInvincibleExit()
    {
        CanEnemiesSeePlayer = true;
    }
    private void ChangeTargetToPlayer()
    {
        SwitchState(EnemyStates.FollowingPlayer);
        animator?.SetTrigger("Walking");
    }
    private void ChangeTargetToDefault()
    {
        targetIndex = 0;
        playerPosForgetTimerCounter = playerPosForgetTimer;
        destinationScript.target = pathPoints[targetIndex];
        animator?.SetTrigger("Walking");

        SwitchState(EnemyStates.WalkingBetweenPoints);
    }
    private void MoveNext()
    {
        if(currentState == EnemyStates.FollowingPlayer)
            destinationScript.target = playerRef;
        else
            destinationScript.target = pathPoints[targetIndex];
    }

    private IEnumerator CanMoveNext()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            float magnitude = (destinationScript.target.position - transform.position).magnitude;
            if (magnitude < 1)
            {
                targetIndex++;
                switch (currentState)
               {
                    case EnemyStates.Staying:                       
                        destinationScript.target = pathPoints[0];
                        break;
                    case EnemyStates.WalkingBetweenPoints:
                        if (targetIndex < pathPoints.Count)
                              MoveNext();
                        else
                            targetIndex = -1;
                            break;
                    case EnemyStates.FollowingPlayer:
                            MoveNext();
                            break;
                }     
            }            
        }
            
    }

}
