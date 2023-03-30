using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    [SerializeField] private float hideTime;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private PlayerWeapon playerWeapon;

    private void Start() => PlayerWeapon.OnComboUpdate += UpdateText;
    
    private void UpdateText() => StartCoroutine(TextUpdateCoroutine());

    private IEnumerator TextUpdateCoroutine()
    {
        comboText.SetText($"Combo X{playerWeapon.KilledInCombo}");
        yield return new WaitForSeconds(hideTime);
        comboText.SetText("");
    }
}
