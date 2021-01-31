using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
	private CharacterState characterState;
	[Range(0, 1)] [SerializeField] private float m_AirSpeed = .75f;          // Amount of maxSpeed applied to air movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	private Rigidbody rb;
	bool isJumping;
	private Vector3 m_Velocity = Vector3.zero;
	bool onLedge;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		characterState = GetComponent<CharacterState>();
	}
	

    public void Move(Vector3 dir, float moveSpeed)
    {
	    // //only control the player if grounded or airControl is turned on
		if (characterState.isGrounded || m_AirControl)
		{
			if(!characterState.isGrounded)
			{
				dir.x *= m_AirSpeed;
				dir.z *= m_AirSpeed;
			}
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector3(dir.x*moveSpeed, rb.velocity.y, dir.z*moveSpeed);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		 }
	}

}
