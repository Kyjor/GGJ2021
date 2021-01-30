using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    

    public class CharacterAnimationController : MonoBehaviour
    {
        private CharacterState characterState;
        private Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            characterState = GetComponent<CharacterState>();
            anim = GetComponentInChildren<Animator>();
            characterState.OnIdleState += PlayIdleAnimation;
            characterState.OnRunState += PlayRunAnimation;
            characterState.OnJumpState += PlayJumpAnimation;
            characterState.OnIdleHoldingItemState += PlayIdleHoldingItemAnimation;
            characterState.OnRunHoldingItemState += PlayRunHoldingItemAnimation;
            characterState.OnJumpHoldingItemState += PlayRunHoldingItemAnimation;
        }

        private void PlayRunHoldingItemAnimation()
        {
            anim.Play("RunHoldingItem");
        }

        private void PlayIdleHoldingItemAnimation()
        {
            //anim.Play("IdleHoldingItem");
            throw new NotImplementedException();
        }

        void PlayIdleAnimation()
        {
            anim.Play("Idle"); 
        }

        void PlayRunAnimation()
        {
            anim.Play("Run");
        }

        void PlayJumpAnimation()
        {
            anim.Play("Jump");
        }

    }
    