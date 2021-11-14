using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //
using UnityEngine.Events; //


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
    

    public ConfigurableJoint hipJoint; //La composante ConfigurableJoint du gameObject Joueur/Character.
    public GameObject gravityController; // GameObject GravityController

/*     [SerializeField]
    private GestionPlayerInput gestionPlayerInput; //Script GestionPlayerInput */

    //Attribution d'une couleur aléatoire - début
    private static Color color1;
    private static Color color2;
    private static bool hasColorsAssigned = false;
    private static string lastColor;

    //Variables TEST
        //Le Input Controller
/*     private PlayerInputActions playerInputActions; */

    //Pour se déplacer
    private Vector2 move;

    private float moveHorizontal;
    private float moveVertical;

/*     //Pour les pouvoirs
    private bool isPushing;
    private bool isAttracting; */

    //Pour le saut
    private bool jumped = false;

    // ===================================================================== **
    // Initialise/Détecte les Inputs.
    // ===================================================================== **
    /*     private void Awake() 
        {
            playerInputActions = new PlayerInputActions();

            //Pour se déplacer
            playerInputActions.Player.Movement.performed += SeeMove;
            playerInputActions.Player.Movement.canceled += SeeMove;

            //Pour le saut
            playerInputActions.Player.Jump.started += SeeJump;

            //Pour repousser
            playerInputActions.Player.Push.started += SeePush;
            playerInputActions.Player.Push.canceled += SeePush;

            //Pour attirer
            playerInputActions.Player.Suck.started += SeeSuck;
            playerInputActions.Player.Suck.canceled += SeeSuck;

        } */


    public GameObject systemeDeParticules;



    // ===================================================================== **
    // Start is called at the start of the game
    // Initialise les variables.
    // ===================================================================== **

    void Start()
    {
        hips = GetComponent<Rigidbody>();

        //gestionPlayerInput.jump.AddListener(Jump);//

        //Gérer les instances
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


    }


    // TEST Gestion des Inputs

        // ===================================================================== **
    // Active les actions du joueur.
    // ===================================================================== **
/*     private void OnEnable()
    {
        playerInputActions.Player.Enable();
    } */

    // ===================================================================== **
    // Désactive les actions du joueur.
    // ===================================================================== **
/*     private void OnDisable()
    {
        playerInputActions.Player.Disable();
    } */

    // ===================================================================== **
    // Fait sauter le joueur dans le script CharacterController.
    // ===================================================================== **
    public void SeeJump(InputAction.CallbackContext context)
    {
        Debug.Log("touche jump");
        jumped = true;
        jumped = context.ReadValueAsButton();
    }

    // ===================================================================== **
    // Fait bouger le joueur dans le script CharacterController.
    // ===================================================================== **
    public void SeeMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();

        moveHorizontal = move.x;
        moveVertical = move.y;

        Debug.Log("move");
    }



    // ===================================================================== **
    // FixedUpdate is called once per frame
    // Détermine le déplacement du joueur en fonction de ses Inputs.
    // ===================================================================== **
    void FixedUpdate()
    {
        if (isGrounded && jumped)
        {
            hips.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
            jumped = false;
        }

        MoveCharacter();
        RotateCharacter();
        
    }

    // ===================================================================== **
    // Déplacement du joueur.
    // ===================================================================== **
    private void MoveCharacter() {

/*         float x = gestionPlayerInput.moveHorizontal;//
        float y = gestionPlayerInput.moveVertical;// */

        float x = moveHorizontal;//
        float y = moveVertical;//

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

/*         float x = gestionPlayerInput.moveHorizontal;//
        float y = gestionPlayerInput.moveVertical;// */

        float x = moveHorizontal;//
        float y = moveVertical;//

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
    /*     private void Jump()
        {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        } */

    // ===================================================================== **
    // Mort du joueur.
    // ===================================================================== **

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "vide")
        {
            Instantiate(systemeDeParticules, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            Invoke("DestroyParticules", 5);
            Invoke("RespawnPlayer", 5);

        }

        if (collision.transform.tag == "trappe")
        {
            Instantiate(systemeDeParticules, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
    }
    private void DestroyParticules()
    {
        Destroy(GameObject.Find("confetti(Clone)"));
    }

    // ===================================================================== **
    // Réaparition du joueur.
    // ===================================================================== **

    private void RespawnPlayer()
    {
        gameObject.transform.position = new Vector3(Random.Range(30, 40), Random.Range(33, 42), Random.Range(17, 23));
    }

























    
}

