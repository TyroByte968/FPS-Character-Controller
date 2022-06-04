//By Tyrobyte
//2021


using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour
  {
    
    #region VARIABLES

    [Header("Player Movement")]
    [Space(5)]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float CrouchGroundCheckHeight = 0f;
    public Vector3 CrouchScale = new Vector3(0.9f,0.9f,0.9f);
    [Space(5)]
    public float jumpSpeed = 8.0f;
    float airTime;
    public float gravity = 20.0f;

    [Space(5)]
    [Header("Camera")]
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public float Swayamount = 2f;
    public float Swaymaxamount = 2f;
    public float Swaysmooth = 3;
    public Transform Camera_Pivot;

    [Space(5)]
    [Header("------Conditions------")]
    public bool canMove = true;
    public bool canMoveCamera = true;
    public bool CanJump = true;
    public bool CanApplyGravity = true ;
    public bool CanCrouch = true;
    [Space(5)]
    public bool IsRunning;
    public bool _IsGrounded;
    public bool IsCrouching;
    public CharacterController characterController;
    public CharacterFootsteps FootstepManager;

    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    Vector3 DefScale;
    //----------------------------------//
    float rotationX = 0;
    float defcheckheight;
    private Quaternion def;
    //----------------------------------//

    #endregion

   void Start()
    {
        defcheckheight = FootstepManager.groundCheckHeight;
        characterController = GetComponent<CharacterController>();
        DefScale = transform.lossyScale;

    }
   
   void Update()
    {

        _IsGrounded = characterController.isGrounded;
        CheckLand();
        CheckAirTime();
        Crouch();
        Apply_Gravity();
        Rotate_Camera();
        Movement_Logic();
       
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Crouch()
    {
        IsCrouching = Input.GetButton("Crouch");

        if(IsCrouching&&CanCrouch)
        {
            transform.localScale = CrouchScale;
            FootstepManager.groundCheckHeight = CrouchGroundCheckHeight;
        }
        else
        {
            transform.localScale = DefScale;
            FootstepManager.groundCheckHeight = defcheckheight;
        }
    }

    private void Movement_Logic()
    {
         #region Movement

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        IsRunning = Input.GetButton("Sprint");
        
        float curSpeedX = canMove ? (IsRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (IsRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Jump_Logic

        if (Input.GetButton("Jump") && canMove && _IsGrounded&&CanJump)
        {
            moveDirection.y = jumpSpeed;
            
        }
        else
        {
            moveDirection.y = movementDirectionY;
            
        }

        #endregion
    }


    private void Rotate_Camera()
    {
        if (canMoveCamera)
        {
            //Sway logic, Will link source to this code if I find it sometime soon.
            def = transform.rotation;
            float factorZ = -(Input.GetAxis("Horizontal")) * Swayamount;
        
            if (factorZ > Swaymaxamount)
            factorZ = Swaymaxamount;
  
            if (factorZ < -Swaymaxamount)
            factorZ = -Swaymaxamount;
  
        
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera_Pivot.transform.localRotation = Quaternion.Euler(rotationX, 0, def.z+factorZ);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
    }


    private void Apply_Gravity()
    {
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as acceleration (ms^-2)
        if (!_IsGrounded&&CanApplyGravity)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

    }

    private void CheckAirTime() 
    {
     
     //Check if player has jumped, use that calc for determining if player has reached the ground
     if (characterController.isGrounded)
     {
         airTime = 0f;
     }
     else if(!characterController.isGrounded&&Input.GetButton("Jump"))
     {
         airTime += Time.deltaTime;
     }

    }

    private void CheckLand()
    {

     if (airTime > 0)
     {
         if (characterController.isGrounded)
         {
             if (characterController.collisionFlags == CollisionFlags.Below) 
             {
                Debug.Log("Landed");
                //Spawn Landing particles here, etc
             }
         }
     }

    }


    //------------------Helper Methods------------------//


    //Disable/Enable Camera Movement Immediatley
    public void SetCameraImmediate(bool b)
    {
        canMoveCamera = b;
    }

    //Disable/Enable Complete Movement (Camera && Player) with/without time delay
    public void Set_Movement(float delay, bool b)
    {
        StartCoroutine(SetMove(delay,b));
    }


    //IEnumerator Called by Set_Movement
    private IEnumerator SetMove(float t, bool _b)
    {
        yield return new WaitForSeconds(t);
        canMove = _b;
        canMoveCamera = _b;
        yield break;
    }




}