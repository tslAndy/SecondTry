using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] Shooting shooting;

    private GameObject playerObject;
    private Player player;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    private void Update()
    {
        if (fieldOfView.CanSeePlayer)
        {
            Debug.Log("should shoot");
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Debug.Log($"{player.IsInShadow} {player.Invisible}");
            if (!(player.IsInShadow || player.Invisible))
                shooting.Shoot(direction);
        }
    }
}
