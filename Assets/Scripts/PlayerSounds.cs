using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource stepSound, attackSound;

    private void PlayStepsSound() => stepSound.Play();
    private void PlayAttackSound() => attackSound.Play();


}
