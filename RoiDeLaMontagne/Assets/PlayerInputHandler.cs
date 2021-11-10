using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    CubeController player;
    private bool jumped = false;

    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform.position, transform.rotation).GetComponent<CubeController>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        if(player)
        player.OnMove(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context){
        jumped = true;
        jumped = context.ReadValue<bool>();
        jumped = context.action.triggered;
    }
}
