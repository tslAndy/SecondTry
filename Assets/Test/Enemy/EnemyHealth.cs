using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public delegate void DeathAction();
    public static event DeathAction OnDeathAction;

    public GameObject boomEffect;
    public GameObject attackObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        StartCoroutine(PlayAnimAndDestroy());
    }

    IEnumerator PlayAnimAndDestroy()
    {
        attackObject.SetActive(false);
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        OnDeathAction.Invoke();
        Destroy(gameObject);
    }
}
