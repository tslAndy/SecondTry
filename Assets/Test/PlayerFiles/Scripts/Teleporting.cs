using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    [SerializeField]
    float energyCost;

    [SerializeField]
    float teleportDuration;

    [SerializeField]
    GameObject teleportPrefab;

    [SerializeField]
    Player player;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    private GameObject _lastTeleport;

    private void Update()
    {
        if (!Input.GetButtonDown("Fire3"))
            return;

        if (_lastTeleport == null)
            SpawnTeleport();
        else
            StartCoroutine(TeleportCoroutine());
    }

    private void SpawnTeleport()
    {
        if (player.CurrentEnergyAmount - energyCost < 0)
            return;
        _lastTeleport = Instantiate(teleportPrefab, transform);
        _lastTeleport.transform.SetParent(null);
        _lastTeleport.GetComponent<DestroyableObstacle>().TriggerDestroying();
        player.CurrentEnergyAmount -= energyCost;
    }

    private IEnumerator TeleportCoroutine()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(teleportDuration);
        transform.position = _lastTeleport.transform.position;
        spriteRenderer.enabled = enabled;
        Destroy(_lastTeleport);
    }
}
