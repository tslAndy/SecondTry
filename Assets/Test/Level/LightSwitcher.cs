using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightSwitcher : MonoBehaviour
{
    [SerializeField]
    private float switchDuration;

    [SerializeField]
    Light2D[] lights;

    private bool _switching;

    public delegate void LightTurnOffAction();
    public static event LightTurnOffAction OnLightTurnOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        if (_switching)
            return;
        StartCoroutine(SwitchCoroutine());
    }

    private IEnumerator SwitchCoroutine()
    {
        _switching = true;
        SwitchLights();
        yield return new WaitForSeconds(switchDuration);
        SwitchLights();
        _switching = false;
    }

    private void SwitchLights()
    {
        foreach (Light2D light in lights)
            light.enabled = !light.enabled;
        OnLightTurnOff.Invoke();
    }
}
