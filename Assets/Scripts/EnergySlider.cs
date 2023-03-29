using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;

    private void Update() => slider.value = player.CurrentEnergyAmount / player.StartEnergyAmount;
}
