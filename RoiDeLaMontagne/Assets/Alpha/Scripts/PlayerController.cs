using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [SerializeField]
    private GestionPlayerInput gestionPlayerInput; //Script GestionPlayerInput

    // ===================================================================== **
    // Start is called at the start of the game
    // Initialise les variables.
    // ===================================================================== **
    void Start()
    {
        hips = GetComponent<Rigidbody>();

        gestionPlayerInput.jump.AddListener(Jump);


    }

    // ===================================================================== **
    // FixedUpdate is called once per frame
    // Détermine le déplacement du joueur en fonction de ses Inputs.
    // ===================================================================== **
    private void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();
        
    }

    // ===================================================================== **
    // Déplacement du joueur.
    // ===================================================================== **
    private void MoveCharacter() {

        float x = gestionPlayerInput.moveHorizontal;
        float y = gestionPlayerInput.moveVertical;

        Vector3 direction = new Vector3(x, 0, y).normalized;

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

        float x = gestionPlayerInput.moveHorizontal;
        float y = gestionPlayerInput.moveVertical;

        Vector3 direction = new Vector3(x, 0, y).normalized;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        Quaternion startRotation = hipJoint.transform.rotation;

        if (direction.magnitude >= 0.1f) {     

            // Détermine l'orientation 
            if (x > 0)
            {
                rotationVector = new Vector3 (0, 0, 0);
            }

            if (y > 0)
            {
                rotationVector = new Vector3 (0, 270, 0);
            }

            if (y < 0)
            {
                rotationVector = new Vector3 (0, 90, 0);
            }

            if (x < 0)
            {
                rotationVector = new Vector3 (0, 180, 0);
            }

            // La rotation est refusée si le joueur n'est pas en contact avec un autre joueur lorsqu'il utilise l'aspirateur.
            if (!gravityController.GetComponent<Gravity>().isTouchingPlayer && gravityController.GetComponent<Gravity>().isAttracting) {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 0);
            } else {
                hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 10 * Time.fixedDeltaTime);
            }

            
        }
    }

    // ===================================================================== **
    // Gère le saut du joueur.
    // ===================================================================== **
    private void Jump()
    {
        if (isGrounded)
        {
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }

    /*     private void GetCurrentView() {
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
        } */


private void OnCollisionEnter(Collision collision) {
        if(collision.transform.tag == "vide")
        {
            Debug.Log("Mort");
            gameObject.transform.position = new Vector3(Random.Range(30, 40), Random.Range(33, 42), Random.Range(17, 23));
        }
            
    }
    
}

