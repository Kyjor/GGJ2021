using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
	#region variables

	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, 1)] [SerializeField] private float m_AirSpeed = .75f;          // Amount of maxSpeed applied to air movement. 1 = 100%

	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody rb;
	bool isJumping;
	private Vector3 m_Velocity = Vector3.zero;
	bool onLedge;

	public GameObject characterHolder;

 #endregion
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnLeaveGroundEvent;
   
    [System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}
		if(OnLeaveGroundEvent == null)
		{
			OnLeaveGroundEvent = new UnityEvent();
		}
	}
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		GroundCollisionChecks();
	}
	void GroundCollisionChecks()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		Collider[] groundColliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < groundColliders.Length; i++)
		{
			if (groundColliders[i].gameObject != gameObject)
			{

				m_Grounded = true;

				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
				}
			}
		}
		if(m_Grounded == false && wasGrounded ==  true)
		{
			OnLeaveGroundEvent.Invoke();
//			print("Left Ground");
		}
	}

    #region horizontalmove
    public void Move(Vector3 dir, float moveSpeed)
    {
	    // //only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			rb.AddForce(dir * Time.deltaTime * moveSpeed);
		// 	if(!m_Grounded)
		// 	{
		// 		velocityX *= m_AirSpeed;
		// 		velocityZ *= m_AirSpeed;
		// 	}
		// 	// Move the character by finding the target velocity
		// 	Vector3 targetVelocity = new Vector3(velocityX, m_Rigidbody.velocity.y, velocityZ);
		// 	// And then smoothing it out and applying it to the character
		// 	m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		 }
	}
    #endregion

}
