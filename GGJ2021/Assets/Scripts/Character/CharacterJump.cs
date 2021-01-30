using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private bool isJumpingForTheFirstTime = false;

	public float characterHighestPoint = 0f;
	public Transform characterHolder;
	public int jumpsLeft = 1;
	Rigidbody rb;
	private CharacterMovement characterMovement;
	private CustomGravity customGravity;

	// Start is called before the first frame update
	void Start()
    {
		characterMovement = GetComponent<CharacterMovement>();
		rb = GetComponent<Rigidbody>();
		
		customGravity = GetComponent<CustomGravity>();
		characterHighestPoint = transform.position.y;
		jumpsLeft = 1;
    }
	
	public void Jump(float velocity, bool jump)
	{	
	
		if (characterMovement.m_Grounded  && jump && jumpsLeft > 0)
		{
			isJumpingForTheFirstTime = true;
				rb.velocity += new Vector3(0f, velocity);
		}
		else if (!characterMovement.m_Grounded && jump && jumpsLeft > 0)
		{
			characterHighestPoint = transform.position.y; //When we are double jumping, the highest point needs to be reset to mitigate fall damage
			rb.velocity = new Vector3(rb.velocity.x, 0f);
			rb.velocity += new Vector3(0f, velocity);
			--jumpsLeft;
		}
		
	}
	IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
	{
		Vector3 originalSize = transform.localScale;
		Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
		float t = 0f;
		while (t <= 1.0)
		{
			t += Time.deltaTime / seconds;
			transform.localScale = Vector3.Lerp(originalSize, newSize, t);
			yield return null;
		}
	}

	public void LandingReset()
    {
	    isJumpingForTheFirstTime = false;
	    characterHighestPoint = transform.position.y;
		jumpsLeft = 1;
    }
	public void OnLeaveGround()
	{
		--jumpsLeft;
	}

}
