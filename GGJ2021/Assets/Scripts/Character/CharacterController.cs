using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CharacterController : MonoBehaviour
{
    CharacterMovement characterMovement;
    CharacterJump characterJump;
    private CinemachineFreeLook freeLook;
    public Transform cam;
    
    public float moveSpeed = 1000f;
    public float jumpSpeed = 10f;
    protected Vector2 movementAxes;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    
    protected bool jump;
    protected bool sprint;
    
    public delegate void SetAxes(Vector2 axes, bool isSprinting);
    public SetAxes axesDelegate;
    [SerializeField] private float sprintSpeed = 10f;

    public virtual void Start()
    {
        freeLook = GetComponentInChildren<CinemachineFreeLook>();
        characterMovement = GetComponent<CharacterMovement>();
        characterJump = GetComponent<CharacterJump>();
    }
    public virtual void Update() {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterJump.Jump(jumpSpeed, jump);

        Vector3 direction = new Vector3 (movementAxes.x, 0, movementAxes.y).normalized;
        
            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                float speedAdded = sprint ? sprintSpeed : 0;
                characterMovement.Move(moveDir.normalized, moveSpeed + speedAdded);
            }
            
            jump = false;
            sprint = false;
    }
}
