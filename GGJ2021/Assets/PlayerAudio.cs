using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private CharacterState characterState;
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] footStepClips;
    [SerializeField] private AudioClip jumpClip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        characterState = GetComponentInParent<CharacterState>();
    }

    private void Start()
    {
        characterState.OnLandAction += Step;
        characterState.OnJumpState += Jump;
    }

    // Update is called once per frame
    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private void Jump()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    private AudioClip GetRandomClip()
    {
        return footStepClips[UnityEngine.Random.Range(0, footStepClips.Length)];
    }
}
