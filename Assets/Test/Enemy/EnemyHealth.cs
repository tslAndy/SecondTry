using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public delegate void DeathAction();
    public static event DeathAction OnDeathAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        OnDeathAction.Invoke();
        Destroy(gameObject);
    }
}
