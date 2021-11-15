using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    //INPUT_ACTIONS

    //Pour se déplacer
    private Vector2 move;

    private float moveHorizontal;
    private float moveVertical;

    private bool isButtonPush;
    private bool isButtonSuck;

    // ===================================================================== **
    // Détecte si le joueur pousse.
    // ===================================================================== **
    public void SeePush(InputAction.CallbackContext context)
    {
        isButtonPush = context.ReadValueAsButton();
    }

    // ===================================================================== **
    // Détecte si le joueur attire un autre joueur/objet.
    // ===================================================================== **
    public void SeeSuck(InputAction.CallbackContext context)
    {
        isButtonSuck = context.ReadValueAsButton();
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
    // Update is called once per frame
    // Détermine l'action du joueur en fonction de ses Inputs.
    // ===================================================================== **

    void FixedUpdate()
    {
        //Détecte si le joueur pousse
        if (isButtonPush) {
            isPushing = true;
        } else {
            isPushing = false;
        }

        //Détecte si le joueur attire
        if (isButtonSuck) {
            isAttracting = true;
        } else {
            isAttracting = false;
        }

        ChangeGravityDirection();
    }


    // ===================================================================== **
    // Change la direction de la force appliquée par le joueur
    // (lorsqu'il souffle) en fonction de ses mouvements.
    // ===================================================================== **

    private void ChangeGravityDirection() {
        int gravityValue = 6;

        float x = moveHorizontal;
        float y = moveVertical;

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
