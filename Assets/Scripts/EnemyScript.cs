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
    private int targetIndex = 0;

    private AIPath pathScript;
    private AIDestinationSetter destinationScript;
    void Start()
    {
        pathScript = GetComponent<AIPath>();
        destinationScript = GetComponent<AIDestinationSetter>();

        if (!shouldEnemyStay)
            destinationScript.target = pathPoints[targetIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(!shouldEnemyStay)
            CanMoveNext();
    }

    private void MoveNext()
    {
        destinationScript.target = pathPoints[targetIndex];
    }

    private void CanMoveNext()
    {
        float magnitude = (destinationScript.target.position - transform.position).magnitude;
        Debug.Log(targetIndex);
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
