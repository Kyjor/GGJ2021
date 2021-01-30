using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    CharacterMovement characterMovement;
    CharacterJump characterJump;
    private CinemachineFreeLook freeLook;
    public Transform cam;
    
    public float moveSpeed = 1000f;
    public float jumpSpeed = 10f;
    protected Vector2 movementAxes;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

       //min and max the Y axis can go. This prevents the camera from being able to flip upside down
	protected float minimumY = -60F;
	protected float maximumY = 60F;

    //Initializing variable for the current rotation of each axis
	public float rotationY = 0F;
    public float rotationX = 0F;

    //How fast the player can move each axis
	public float sensitivityX = 0.1F;
	public float sensitivityY = 0.1F;

    // Start is called before the first frame update

    public bool canMove;
    protected bool jump;
    
    public delegate void SetAxes(Vector2 axes);
    public SetAxes axesDelegate;

    public virtual void Start()
    {
        freeLook = GetComponentInChildren<CinemachineFreeLook>();
        characterMovement = GetComponent<CharacterMovement>();
        characterJump = GetComponent<CharacterJump>();
        rb = GetComponent<Rigidbody>();
    }
    public virtual void Update() {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterJump.Jump(jumpSpeed, jump);

        Vector3 direction = new Vector3 (movementAxes.x, 0, movementAxes.y).normalized;

        if(canMove)
        {
            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //Move(moveDir.normalized);
                characterMovement.Move(moveDir.normalized, moveSpeed);
            }
        }

        freeLook.m_XAxis.m_InputAxisValue = rotationX;
        freeLook.m_YAxis.m_InputAxisValue = rotationY;
        jump = false;
    }
}
