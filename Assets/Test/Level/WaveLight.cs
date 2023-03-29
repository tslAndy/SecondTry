using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaveLight : MonoBehaviour
{
    [SerializeField] private float delta, minValue;
    [SerializeField] private Light2D light2d;

    private float _value;

    private void Update()
    {
        _value += delta * Time.deltaTime;
        light2d.intensity = Mathf.Abs(Mathf.Sin(_value)) + minValue;
        _value %= 1000;
    }
}
