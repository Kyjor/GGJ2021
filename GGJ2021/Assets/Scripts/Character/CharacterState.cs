using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class CharacterState : MonoBehaviour
    {
        private CharacterGroundedCheck characterGroundedCheck;
        private CharacterController characterController;

        
        public Vector2 movementAxes;
        public bool isGrounded;
        public bool isHoldingItem;
        public bool isSprinting;
        public event Action OnIdleState;
        public event Action OnRunState;
        public event Action OnSprintState;
        public event Action OnJumpState;
        public event Action OnIdleHoldingItemState;
        public event Action OnRunHoldingItemState;
        public event Action OnJumpHoldingItemState;
        public event Action OnLandAction;
        
        
        public enum PlayerState
        {
            Idle = 0,
            Run = 1,
            Jump = 2,
            IdleHoldingItem = 3,
            RunHoldingItem = 4,
            JumpHoldingItem = 5, 
            Sprint = 6,
        };
        public PlayerState myPlayerState;

        void Start()
        {
            characterGroundedCheck = GetComponent<CharacterGroundedCheck>();
            characterController = GetComponent<CharacterController>();
            InitializeDelegates();
        }

        private void Update()
        {
            if (isGrounded)
            {
                var moveMagnitude = Mathf.Abs(movementAxes.magnitude);
                int selectedState = moveMagnitude > 0.1f
                    ? (isHoldingItem ? 4 : (isSprinting ? 6 : 1))
                    : (isHoldingItem ? 3 : 0);
                ChangeState(selectedState);
            }
        }
        
        void InitializeDelegates()
        {
            characterGroundedCheck.onLeaveGroundDelegate += () =>
            {
                isGrounded = false;
                int selectedState = isHoldingItem ? 5 : 2;
                ChangeState(selectedState);
            };
            characterGroundedCheck.onLandDelegate += () =>
            {
                OnLandAction?.Invoke();
                isGrounded = true;
            };
            characterController.axesDelegate += (vec2, sprinting) => { movementAxes = vec2;
                isSprinting = sprinting;
            };
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
            if (IsState(state))
            {
                return;
            }
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
                case 3:
                    OnIdleHoldingItemState?.Invoke();
                    break; 
                case 4:
                    OnRunHoldingItemState?.Invoke();
                    break;
                case 5:
                    OnJumpHoldingItemState?.Invoke();
                    break; 
                case 6:
                    OnSprintState?.Invoke();
                    break;
                default:
                    print("State does not exist!");
                    break;
            }
        }
    }

