using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterGroundedCheck : MonoBehaviour
    {
        private CharacterState characterState;

        [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
        [SerializeField] private Transform groundCheck; // A position marking where to check if the player is grounded.
        const float groundedRadius = .15f; // Radius of the overlap circle to determine if grounded

        public delegate void OnLeaveGround();

        public OnLeaveGround onLeaveGroundDelegate;

        public delegate void OnLand();

        public OnLeaveGround onLandDelegate;

        // Start is called before the first frame update
        void Start()
        {
            characterState = GetComponent<CharacterState>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            GroundCollisionChecks();
        }

        void GroundCollisionChecks()
        {
            bool wasGrounded = characterState.isGrounded;
            characterState.isGrounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            Collider[] groundColliders =
                Physics.OverlapSphere(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < groundColliders.Length; i++)
            {
                if (groundColliders[i].gameObject != gameObject)
                {
                    characterState.isGrounded = true;
                    if (!wasGrounded)
                    {
                        onLandDelegate?.Invoke();
                    }
                }
            }

            if (!characterState.isGrounded && wasGrounded)
            {
                onLeaveGroundDelegate?.Invoke();
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(groundCheck.position, groundedRadius);
        }
    }
    