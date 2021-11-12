using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GestionPlayerInput : MonoBehaviour
{
    //Le Input Controller
    private PlayerInputActions playerInputActions;

    //Pour se déplacer
    [SerializeField]
    private Vector2 move;

    public float moveHorizontal;
    public float moveVertical;

    //Pour les pouvoirs
    public bool isPushing;
    public bool isAttracting;

    //Pour le saut
    public UnityEvent jump;

    // ===================================================================== **
    // Initialise/Détecte les Inputs.
    // ===================================================================== **
    private void Awake() 
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

    }

    // ===================================================================== **
    // Active les actions du joueur.
    // ===================================================================== **
    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    // ===================================================================== **
    // Désactive les actions du joueur.
    // ===================================================================== **
    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    // ===================================================================== **
    // Fait sauter le joueur dans le script CharacterController.
    // ===================================================================== **
    private void SeeJump(InputAction.CallbackContext context)
    {
        jump.Invoke();
    }

    // ===================================================================== **
    // Fait bouger le joueur dans le script CharacterController.
    // ===================================================================== **
    private void SeeMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();

        moveHorizontal = move.x;
        moveVertical = move.y;
    }

    // ===================================================================== **
    // Détecte si le joueur pousse dans le script Gravity.
    // ===================================================================== **
    private void SeePush(InputAction.CallbackContext context)
    {
        isPushing = context.ReadValueAsButton();
    }

    // ===================================================================== **
    // Détecte si le joueur attire dans le script Gravity.
    // ===================================================================== **
    private void SeeSuck(InputAction.CallbackContext context)
    {
        isAttracting = context.ReadValueAsButton();
    }
}
