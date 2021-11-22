using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //
using UnityEngine.Events; //

// ===================================================================== **
    // Ce script est à mettre sur le gameObject Joueur/Character.
    // Il permet au joueur de: 
    //      - se déplacer
    //      - rotationner
    //      - mourir
    //      - réaparaître
    // ===================================================================== **


public class PlayerController : MonoBehaviour
{
    


    

    public Rigidbody hips; // gameObject Joueur/Character
    public bool isGrounded;

    public ConfigurableJoint hipJoint; //La composante ConfigurableJoint du gameObject Joueur/Character.
    public GameObject gravityController; // GameObject GravityController

    //Attribution d'une couleur aléatoire - début
    private static Color color1;
    private static Color color2;
    private static bool hasColorsAssigned = false;
    private static string lastColor;

    //Pour se déplacer
    private Vector2 move;
    public float speed;
    public float strafeSpeed;
    

    private float moveHorizontal;
    private float moveVertical;

    //Pour le saut
    private bool jumped = false;
    public float jumpForce;


    //Pour la mort
    public GameObject systemeDeParticules;
    private bool isDead = false;

    //Pour le score
    private GameManager scoreManager;
    private float areaPoints = 10;
    private int multiplicateur = 1;
    private float defaultPoints = 10;
    private int id;
    public bool doubleScore;



    // ===================================================================== **
    // Start is called at the start of the game
    // Initialise les variables.
    // ===================================================================== **

    void Start()
    {
        hips = GetComponent<Rigidbody>();

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

         scoreManager = FindObjectOfType<GameManager>();
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

        doubleScore = GameObject.Find("temps").GetComponent<CountDown>().doublePoint;

        //Section de méthodes appelées sans arrêts
        MoveCharacter();
        RotateCharacter();
        VerifieTemps();
        VerifieMort();
    }


    // ===================================================================== **
    // Fait sauter le joueur dans le script CharacterController.
    // ===================================================================== **
    public void SeeJump(InputAction.CallbackContext context)
    {
            if (isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
    }



    // ===================================================================== **
    // Détecte l'orientation du joueur.
    // ===================================================================== **
    public void SeeMove(InputAction.CallbackContext context)
    {

        move = context.ReadValue<Vector2>();

        moveHorizontal = move.x;
        moveVertical = move.y;

    

    }


    // ===================================================================== **
    // Déplacement du joueur.
    // ===================================================================== **
    private void MoveCharacter() {

        float x = moveHorizontal;
        float y = moveVertical;

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

        float x = moveHorizontal;
        float y = moveVertical;

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
    // Lorsque le joueur entre en coliision avec un objet
    // ===================================================================== **
    private void OnTriggerEnter(Collider collision)
    {
        id = GameManager.playerList[transform.root.gameObject];
    
        
          if (collision.transform.tag == "Niveau5")
           {
            Debug.Log("Niveau5");
            scoreManager.setBonusScore(150 * multiplicateur, id);
           }
           if (collision.transform.tag == "Niveau4")
           {
            Debug.Log("Niveau4");
            scoreManager.setBonusScore(100 * multiplicateur, id);
           }
            if (collision.transform.tag == "Niveau3")
            {
            Debug.Log("Niveau3");
            scoreManager.setBonusScore(70 * multiplicateur, id);
            }
            if (collision.transform.tag == "Niveau2")
            {
            Debug.Log("Niveau2");
            scoreManager.setBonusScore(50 * multiplicateur, id);
            }
            if (collision.transform.tag == "Niveau1")
            {
            Debug.Log("Niveau1");
            scoreManager.setBonusScore(30 * multiplicateur, id);
            }

        if (collision.transform.tag == "vide")
        {
            Instantiate(systemeDeParticules, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            Invoke("DestroyParticules", 5);
            Invoke("RespawnPlayer", 5);
            isDead = true;
            
        }

        if (collision.transform.tag == "trappe")
        {
            Instantiate(systemeDeParticules, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            isDead = true;
        }
    }


    void OnTriggerExit(Collider collision){
        id = GameManager.playerList[transform.root.gameObject];
         if (collision.gameObject.tag == "Niveau5")
         {
            Debug.Log("Exit Niveau5");
            scoreManager.setBonusScore(defaultPoints, id);                        
         }
        if (collision.gameObject.tag == "Niveau4")
        {
            Debug.Log("Exit Niveau4");
            scoreManager.setBonusScore(defaultPoints, id);
        }
        if (collision.gameObject.tag == "Niveau3")
        {
            Debug.Log("Exit Niveau3");
            scoreManager.setBonusScore(defaultPoints, id);
        }
        if (collision.gameObject.tag == "Niveau2")
        {
            Debug.Log("Exit Niveau2");
            scoreManager.setBonusScore(defaultPoints, id);
        }
        if (collision.gameObject.tag == "Niveau1")
        {
            Debug.Log("Exit Niveau1");
            scoreManager.setBonusScore(defaultPoints, id);
        }

    }

    // ===================================================================== **
    // Vérifie si le double points est actif
    // ===================================================================== **
    private void VerifieTemps()
    {
        if (doubleScore == true)
        {

            multiplicateur = 2;
            defaultPoints = 10 * multiplicateur;
        }
    }


    // ===================================================================== **
    // Vérifie si le personnage est mort 
    // ===================================================================== **
    private void VerifieMort()
    {
        if(isDead == true)
        {
            gameObject.SetActive(false);
            scoreManager.setBonusScore(0, id);

        }
    }

    // ===================================================================== **
    // Destruction des particules 
    // ===================================================================== **
    private void DestroyParticules()
    {
        Destroy(GameObject.Find("confetti(Clone)"));
    }

    // ===================================================================== **
    // Réaparition du joueur.
    // ===================================================================== **
    private void RespawnPlayer()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(Random.Range(30, 40), Random.Range(33, 42), Random.Range(17, 23));
        isDead = false;
    }


    

























    
}

