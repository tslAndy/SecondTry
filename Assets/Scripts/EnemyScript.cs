using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> pathPoints = new List<Transform>();
    private int targetIndex = 0;

    private AIPath pathScript;
    private AIDestinationSetter destinationScript;
    void Start()
    {
        pathScript = GetComponent<AIPath>();
        destinationScript = GetComponent<AIDestinationSetter>();

        destinationScript.target = pathPoints[targetIndex];
    }

    // Update is called once per frame
    void Update()
    {
        CanMoveNext();
    }

    private void MoveNext()
    {
        destinationScript.target = pathPoints[targetIndex];
        Debug.LogError("TargetChanged");
    }

    private void CanMoveNext()
    {
        float magnitude = (destinationScript.target.position - transform.position).magnitude;
        Debug.Log(magnitude);
        if (magnitude < 1)
        {
            targetIndex++;
            if (targetIndex < pathPoints.Count)
                MoveNext();
            else
                targetIndex = 0;
        }
    }
}
