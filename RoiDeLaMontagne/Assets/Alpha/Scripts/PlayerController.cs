using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    // ===================================================================== **
    // Ce script est à mettre sur le gameObject Joueur/Character.
    // Il permet au joueur de se déplacer.
    // ===================================================================== **
    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips; // gameObject Joueur/Character
    public bool isGrounded;
    private int jumpAllowed = 0;

    public ConfigurableJoint hipJoint; //La composante ConfigurableJoint du gameObject Joueur/Character.
    public GameObject gravityController; // GameObject GravityController

    private int view = 1;

    PhotonView viewPhoton;

    //New variables in testing phase
    private Vector2 movementInput = Vector2.zero;

    // ===================================================================== **
    // Start is called at the start of the game
    // Initialise les variables.
    // ===================================================================== **
    void Start()
    {
        hips = GetComponent<Rigidbody>();
        viewPhoton = GetComponent<PhotonView>();

    }

    //In test phase
    public void OnMove(InputAction.CallbackContext context) {
        movementInput = context.ReadValue<Vector2>();
    }
    //


    // ===================================================================== **
    // FixedUpdate is called once per frame
    // Détermine le déplacement du joueur en fonction de ses Inputs.
    // ===================================================================== **
    private void FixedUpdate()
    {

        /* Debug.Log(isGrounded); */

        if (viewPhoton.IsMine)
        {
// Saute
        if (Input.GetAxis("Jump") > 0)
        {
            if (isGrounded)
            {
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
            }
           
            
        }
        }

        //
/*         Debug.Log(movementInput.y);
        Debug.Log(movementInput.x); */
        //
        

        GetCurrentView();
        MoveCharacter();
        RotateCharacter();
        
    }

    // ===================================================================== **
    // Déplacement du joueur.
    // ===================================================================== **
    private void MoveCharacter() {

        float horizontal = 0f;
        float vertical = 0f;

        switch (view)
        {
            case 1:
                if (movementInput.y > 0) {
                    vertical = 1f;
                    Debug.Log("to top");
                }

                if (movementInput.y < 0) {
                    vertical = -1f;
                    Debug.Log("to bottom");
                }

                if (movementInput.x > 0) {
                    horizontal = 1f;
                    Debug.Log("to right");
                }

                if (movementInput.x < 0) {
                    horizontal = -1f;
                    Debug.Log("to left");
                }
                break;

            case 2:
                if (Input.GetKey(KeyCode.W)) {
                    horizontal = -1f;
                }

                if (Input.GetKey(KeyCode.S)) {
                    horizontal = 1f;
                }

                if (Input.GetKey(KeyCode.D)) {
                    vertical = 1f;
                }

                if (Input.GetKey(KeyCode.A)) {
                    vertical = -1f;
                }
                break;

            case 3:
                if (Input.GetKey(KeyCode.W)) {
                    vertical = -1f;
                }

                if (Input.GetKey(KeyCode.S)) {
                    vertical = 1f;
                }

                if (Input.GetKey(KeyCode.D)) {
                    horizontal = -1f;
                }

                if (Input.GetKey(KeyCode.A)) {
                    horizontal = 1f;
                }
                break;

            case 4:
                if (Input.GetKey(KeyCode.W)) {
                    horizontal = 1f;
                }

                if (Input.GetKey(KeyCode.S)) {
                    horizontal = -1f;
                }

                if (Input.GetKey(KeyCode.D)) {
                    vertical = -1f;
                }

                if (Input.GetKey(KeyCode.A)) {
                    vertical = 1f;
                }
                break;
            
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Autorise le déplacement lorsque le joueur n'utilise pas l'aspirateur OU utilise l'aspirateur, mais n'est pas en contact avec un autre joueur.
        if (!gravityController.GetComponent<Gravity>().isAttracting || (!gravityController.GetComponent<Gravity>().isTouchingPlayer && gravityController.GetComponent<Gravity>().isAttracting)) {
            hips.AddForce(direction * speed); 

        } else {

            hips.AddForce(direction * 0);
        }
    }

    // ===================================================================== **
    // Fait la rotation du joueur.
    // ===================================================================== **
    private void RotateCharacter() {
/*         float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); */

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W)) {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.S)) {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.D)) {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.A)) {
            horizontal = -1f;
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        Quaternion startRotation = hipJoint.transform.rotation;

        if (direction.magnitude >= 0.1f) {     

            // Détermine l'orientation 
            switch (view)
            {
                case 1:
                    if (Input.GetKey(KeyCode.W))
                    {
                        rotationVector = new Vector3 (0, 0, 0);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        rotationVector = new Vector3 (0, 270, 0);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        rotationVector = new Vector3 (0, 90, 0);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        rotationVector = new Vector3 (0, 180, 0);
                    }
                    break;

                case 2:
                    if (Input.GetKey(KeyCode.W))
                    {
                        rotationVector = new Vector3 (0, 270, 0);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        rotationVector = new Vector3 (0, 180, 0);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        rotationVector = new Vector3 (0, 0, 0);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        rotationVector = new Vector3 (0, 90, 0);
                    }
                    break;

                case 3:
                    if (Input.GetKey(KeyCode.W))
                    {
                        rotationVector = new Vector3 (0, 180, 0);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        rotationVector = new Vector3 (0, 90, 0);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        rotationVector = new Vector3 (0, 270, 0);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        rotationVector = new Vector3 (0, 0, 0);
                    }
                    break;

                case 4:
                    if (Input.GetKey(KeyCode.W))
                    {
                        rotationVector = new Vector3 (0, 90, 0);
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        rotationVector = new Vector3 (0, 0, 0);
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        rotationVector = new Vector3 (0, 180, 0);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        rotationVector = new Vector3 (0, 270, 0);
                    }
                    break;
                
            }

            // La rotation est refusée si le joueur n'est pas en contact avec un autre joueur lorsqu'il utilise l'aspirateur.
            if (!gravityController.GetComponent<Gravity>().isTouchingPlayer && gravityController.GetComponent<Gravity>().isAttracting) {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 0);
            } else {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 10 * Time.fixedDeltaTime);
            }

            
        }
    }

    private void GetCurrentView() {
        Debug.Log(view);

        if (Input.GetKey(KeyCode.Keypad1)) {
            view = 1;
        }

        if (Input.GetKey(KeyCode.Keypad2)) {
            view = 2;
        }

        if (Input.GetKey(KeyCode.Keypad3)) {
            view = 3;
        }

        if (Input.GetKey(KeyCode.Keypad4)) {
            view = 4;
        }
    }

}
