using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{

    private Rigidbody sphereRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;
    AudioSource jumpsound;

    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        jumpsound = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump; 
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        float speed = 20f;
        sphereRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }


    public void Jump(InputAction.CallbackContext context)
    {
        jumpsound.Play();
        Debug.Log(context);
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            sphereRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
       
    }
}
