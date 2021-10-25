using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CubeController : MonoBehaviour
{
     private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

private static Color color1;
     private static Color color2;
     private static bool hasColorsAssigned = false;
     private static string lastColor;

    private void Start()
    {
        
         if (hasColorsAssigned == false)
         {
             color1 = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
             color2 = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
             hasColorsAssigned = true;
         }
         if (lastColor == "color1")
         {
             GetComponent<Renderer>().material.color = color2;
             lastColor = "color2";
         }
         else
         {
             GetComponent<Renderer>().material.color = color1;
             lastColor = "color1";
         }

        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context) {
movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context){
        jumped = true;
jumped = context.ReadValue<bool>();
jumped = context.action.triggered;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
