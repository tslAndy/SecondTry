using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    [HideInInspector]
    public static bool CanEnemiesSeePlayer { get; set; } = true;

    [SerializeField]
    private bool shouldEnemyStay = false;

    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();
    private List<Transform> pathPointsReserve = new List<Transform>();
    private int targetIndex = 0;

    [SerializeField]
    private float playerPosForgetTimer;
    private float playerPosForgetTimerCounter;
    private bool isFollowingPlayer = false;

    private bool isEnenySeeingPlayer;

    private Transform playerRef;

    private AIPath pathScript;
    private AIDestinationSetter destinationScript;
    void Start()
    {
        pathScript = GetComponent<AIPath>();
        destinationScript = GetComponent<AIDestinationSetter>();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
        pathPointsReserve.Add(playerRef);
        playerPosForgetTimerCounter = playerPosForgetTimer;

        Player.PlayerEnteredInvicibility += OnPlayerInvincibleEnter;
        Player.PlayerExitedInvicibility += OnPlayerInvincibleExit;
        if (!shouldEnemyStay)
            destinationScript.target = pathPoints[targetIndex];
    }

    // Update is called once per frame
    void Update()
    {
        isEnenySeeingPlayer = GetComponent<FieldOfView>().CanSeePlayer;
        if (isEnenySeeingPlayer)
        {
            ChangeTargetToPlayer();
        } else if(isFollowingPlayer)
        {
            playerPosForgetTimerCounter -= Time.deltaTime;
            if(playerPosForgetTimerCounter <= 0)
            {
                ChangeTargetToDefault();
            }
        }
        if(!shouldEnemyStay)
            CanMoveNext();
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
        targetIndex = -1;
        isFollowingPlayer = true;
    }
    private void ChangeTargetToDefault()
    {
        Debug.LogWarning(pathPoints[0 ]);
        targetIndex = 0;
        playerPosForgetTimerCounter = playerPosForgetTimer;
        destinationScript.target = pathPoints[targetIndex];
        isFollowingPlayer = false;

    }
    private void MoveNext()
    {
        if(isFollowingPlayer)
            destinationScript.target = pathPointsReserve[targetIndex];
        else
            destinationScript.target = pathPoints[targetIndex];
    }

    private void CanMoveNext()
    {
        float magnitude = (destinationScript.target.position - transform.position).magnitude;
        if (magnitude < 1)
        {
            targetIndex++;
            if (targetIndex < pathPoints.Count)
                MoveNext();
            else
                targetIndex = -1;
        }
    }

}
