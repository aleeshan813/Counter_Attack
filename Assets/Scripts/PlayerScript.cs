using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float PlayerSpeed = 1.9f;
    [SerializeField] float PlayerSprint = 3f;
   
    [Header("Player Camera")]
    [SerializeField] Transform PlayerCamera;

    [Header("Player Animator and Gravity")]
    [SerializeField] CharacterController characterController;
    float gravity = -9.8f;

    [Header("Events")]
    public UnityEvent onPlayerMove; // Define a public UnityEvent variable

    [Header("Player Jumping & Velocity")]
    [SerializeField] float JumpRange = 1.9f;
    [SerializeField] float turnCalmTime = 0.1f;
    [SerializeField] Transform surfaceCheck;
    [SerializeField] LayerMask surfaceMask;

    float turnCalmVelocity;
    Vector3 velocity;
    bool onSurface;
    float sufaceDistance = 0.4f;
   


    private void FixedUpdate()
    {
        onPlayerMove.Invoke();
    }


    // Gravity
    public void Gravity()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, sufaceDistance, surfaceMask);

        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    //Movement
    public void playerMove()
    {
        float Horizontal_axis = Input.GetAxisRaw("Horizontal");
        float Vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 MoveDirection = new Vector3(Horizontal_axis, 0f, Vertical_axis);
        MoveDirection.Normalize();

        if (MoveDirection.magnitude >= 0.1f)
        {
            AnimationManager.instance.SetBool_Anim("Walk", true);

            float targetAngle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 MoveCameraDirection = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward;
            characterController.Move(MoveCameraDirection.normalized * PlayerSpeed * Time.deltaTime);
        }
        else
        {
            AnimationManager.instance.SetBool_Anim("Walk", false) ;
        }
    }

    //Sprint
    public void Sprint()
    {
        if(Input.GetButton("Sprint") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && onSurface)
        {
            AnimationManager.instance.SetBool_Anim("Running", true);
            

            float Horizontal_axis = Input.GetAxisRaw("Horizontal");
            float Vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 MoveDirection = new Vector3(Horizontal_axis, 0f, Vertical_axis);
            MoveDirection.Normalize();

            if (MoveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(MoveDirection.x, MoveDirection.z) * Mathf.Rad2Deg + PlayerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 MoveCameraDirection = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward;
                characterController.Move(MoveCameraDirection.normalized * PlayerSprint * Time.deltaTime);
            }
        }
        else
        {
            AnimationManager.instance.SetBool_Anim("Running", false);
        }
    }

    //Jumping
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            AnimationManager.instance.SetBool_Anim("Walk", false);
            AnimationManager.instance.Set_Trigger_Anim("Jump");
            velocity.y = Mathf.Sqrt(JumpRange * -2 * gravity);
        }
        else
        {
            AnimationManager.instance.Reset_Trigger_Anim("Jump");
        }
    }
}
