using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Enemy"))
        {
            //collision.gameObject.SetActive(false);
            EnemyScript.PlayerDied?.Invoke();
        }
    }
}
