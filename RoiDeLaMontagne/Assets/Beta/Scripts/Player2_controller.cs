using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_controller : MonoBehaviour
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

    public ConfigurableJoint hipJoint; //La composante ConfigurableJoint du gameObject Joueur/Character.
    public GameObject gravityController; // GameObject GravityController

    // ===================================================================== **
    // Start is called at the start of the game
    // Initialise les variables.
    // ===================================================================== **
    void Start()
    {
        hips = GetComponent<Rigidbody>();

    }

    // ===================================================================== **
    // FixedUpdate is called once per frame
    // Détermine le déplacement du joueur en fonction de ses Inputs.
    // ===================================================================== **
    private void FixedUpdate()
    {
        // Saute
        if(Input.GetAxis("Jump") > 0)
        {
            if(isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            
            }
        }

        MoveCharacter();
        RotateCharacter();
    }

    // ===================================================================== **
    // Déplacement du joueur.
    // ===================================================================== **
    private void MoveCharacter() {
/*         float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); */

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontal = -1f;
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

        if (Input.GetKey(KeyCode.UpArrow)) {
            vertical = 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            vertical = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            horizontal = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontal = -1f;
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        Quaternion startRotation = hipJoint.transform.rotation;

        if (direction.magnitude >= 0.1f) {     

            // Détermine l'orientation 
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rotationVector = new Vector3 (0, 270, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotationVector = new Vector3 (0, 180, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotationVector = new Vector3 (0, 0, 0);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rotationVector = new Vector3 (0, 90, 0);
            }

            // La rotation est refusée si le joueur n'est pas en contact avec un autre joueur lorsqu'il utilise l'aspirateur.
            if (!gravityController.GetComponent<Player2_gravity>().isTouchingPlayer && gravityController.GetComponent<Player2_gravity>().isAttracting) {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 0);
            } else {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 10 * Time.fixedDeltaTime);
            }

            
        }
    }

}
