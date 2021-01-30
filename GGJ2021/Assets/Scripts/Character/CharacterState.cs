using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CharacterState : MonoBehaviour
    {
        private CharacterGroundedCheck characterGroundedCheck;
        private CharacterController characterController;
        private CharacterAnimationController characterAnimationController;

        public bool isGrounded;
        public Vector2 movementAxes;

        public event Action OnIdleState;
        public event Action OnRunState;
        public event Action OnJumpState;
        public event Action OnTurn;

        private bool isFacingRight = true;
        public bool isAttacking;

        public enum PlayerState
        {
            Idle = 0,
            Run = 1,
            Jump = 2,
            Melee = 3,
        };

        public PlayerState myPlayerState;

        #region MonoBehaviourCallBacks

        // Start is called before the first frame update
        void Start()
        {
            characterGroundedCheck = GetComponent<CharacterGroundedCheck>();
            characterController = GetComponent<CharacterController>();
            characterAnimationController = GetComponent<CharacterAnimationController>();

            InitializeDelegates();
        }

        private void Update()
        {

            if (isGrounded)
            {
                var moveMagnitude = Mathf.Abs(movementAxes.magnitude);
                if (moveMagnitude > 0.1f && !IsState(1))
                {
                    ChangeState(1);
                }
                else if (moveMagnitude < 0.1f && !IsState(0))
                {
                    ChangeState(0);
                }
            }
        }

        #endregion

        void InitializeDelegates()
        {
            characterGroundedCheck.onLeaveGroundDelegate += () =>
            {
                isGrounded = false;
                ChangeState(2);
            };
            characterGroundedCheck.onLandDelegate += () =>
            {
                isGrounded = true;
            };
            characterController.axesDelegate += (vec2) => { movementAxes = vec2; };
        }

        public bool IsState(int stateIndex)
        {
            if (myPlayerState == (PlayerState) stateIndex)
            {
                return true;
            }

            return false;
        }

        public void ChangeState(int state)
        {
            //We shouldn't change states if we're already in it
            if (myPlayerState == (PlayerState) state)
            {
                return;
            }

            var lastPlayerState = myPlayerState;
            myPlayerState = (PlayerState) state;
            switch (state)
            {
                case 0:
                    OnIdleState?.Invoke();
                    break;
                case 1:
                    OnRunState?.Invoke();
                    break;
                case 2:
                    OnJumpState?.Invoke();
                    break;
                default:
                    print("State does not exist!");
                    break;
            }
        }
        
    }

