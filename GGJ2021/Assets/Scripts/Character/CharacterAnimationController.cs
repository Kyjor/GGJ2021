using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    

    public class CharacterAnimationController : MonoBehaviour
    {
        private CharacterState characterState;
        private Animator anim;
        [SerializeField] private GameObject characterHolder;
        public ParticleSystem dust;
        private bool isSqueezing;

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
            characterState.OnSprintState += PlaySprintAnimation;
            characterState.OnJumpState += PlayJumpAnimation;
            characterState.OnIdleHoldingItemState += PlayIdleHoldingItemAnimation;
            characterState.OnRunHoldingItemState += PlayRunHoldingItemAnimation;
            characterState.OnJumpHoldingItemState += PlayRunHoldingItemAnimation;
            characterState.OnLandAction += () =>
            {
                Squeeze(1.15f, .95f, 0.1f);
                dust.Play();
            }; 
        }

        private void PlaySprintAnimation()
        {
            anim.Play("Sprint");
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
            Squeeze(0.9f, 1.1f, 0.1f);
        }
        public void Squeeze(float xSqueeze, float ySqueeze, float seconds)
        {
            if (isSqueezing)
            {
                return;
            }
            StartCoroutine(JumpSqueeze(xSqueeze, ySqueeze, seconds));
        }
        IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
        {
            
            isSqueezing = true;
            Vector3 originalSize = characterHolder.transform.localScale;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, xSqueeze);
            float t = 0f;
            while (t <= seconds)
            {
                t += Time.deltaTime;
                characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t/seconds);
                yield return null;
            }

            characterHolder.transform.localScale = newSize;
            t = 0f;
            while (t < seconds)
            {
                t += Time.deltaTime;
                characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t/seconds);
                yield return null;
            }

            characterHolder.transform.localScale = originalSize;
            isSqueezing = false;
        }

    }
    