using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private bool shouldEnemyStay = false;

    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();
    private List<Transform> pathPointsReserve = new List<Transform>();
    private int targetIndex = 0;

    [SerializeField]
    private float playerPosForgetTimer;

    private bool isEnenySeeingPlayer;

    private Transform playerRef;

    private AIPath pathScript;
    private AIDestinationSetter destinationScript;
    void Start()
    {
        pathScript = GetComponent<AIPath>();
        destinationScript = GetComponent<AIDestinationSetter>();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;

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
            Debug.LogWarning("ChangingToPlayer");
        }
        if(!shouldEnemyStay)
            CanMoveNext();
    }

    private void ChangeTargetToPlayer()
    {
        pathPointsReserve = pathPoints;
        pathPoints.Clear();
        pathPoints.Add(playerRef);
    }
    private void MoveNext()
    {
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
