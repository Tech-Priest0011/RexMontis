using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // ===================================================================== **
    // Ce script est à mettre sur le gameObject GravityController.
    // Il est enfant du joueur. 
    // Il détecte les inputs/actions du joueur.
    // ===================================================================== **

    public float strength; // Il faut déterminer la valeur avec Unity. Elle réutilisée dans le script GravityController.
    public Vector3 direction;

    public bool isPushing = false;
    public bool isAttracting = false;

    public bool isTouchingPlayer = false;

    //Testing Phase
    [SerializeField]
    private GestionPlayerInput gestionPlayerInput;

    void Start()
    {

        //
        //gestionPlayerInput.isPushing.AddListener(Jump);
        Debug.Log(gestionPlayerInput.isPushing);
        //

    }

    // ===================================================================== **
    // Update is called once per frame
    // Détermine l'action du joueur en fonction de ses Inputs.
    // ===================================================================== **

    void FixedUpdate()
    {
        isPushing = gestionPlayerInput.isPushing;

        isAttracting = gestionPlayerInput.isAttracting;

        Debug.Log(isAttracting);

        ChangeGravityDirection();
    }

    // ===================================================================== **
    // Change la direction de la force appliquée par le joueur
    // (lorsqu'il souffle) en fonction de ses mouvements.
    // ===================================================================== **

    private void ChangeGravityDirection() {
        int gravityValue = 6;

        float x = gestionPlayerInput.moveHorizontal;
        float y = gestionPlayerInput.moveVertical;

        if (y > 0) {
            direction = new Vector3 (0, 0, gravityValue);
        }

        if (x > 0) {
            direction = new Vector3 (gravityValue, 0, 0);
        }

        if (y < 0) {
            direction = new Vector3 (0, 0, -gravityValue);
        }

        if (x < 0) {
            direction = new Vector3 (-gravityValue, 0, 0);
        }
    }

    // ===================================================================== **
    // Détecte l'entrée en collision avec un autre joueur.
    // ===================================================================== **
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            isTouchingPlayer = true;
        }
    }

    // ===================================================================== **
    // Détecte la sortie d'un joueur dans la zone de collision.
    // ===================================================================== **
    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "Player") {
            isTouchingPlayer = false;
        }
    }
}
