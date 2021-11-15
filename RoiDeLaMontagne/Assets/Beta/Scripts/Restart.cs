using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //
using UnityEngine.Events; //
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    private string quelleScene;

    // Update is called once per frame
    void Update()
    {
        quelleScene = SceneManager.GetActiveScene().name;
    }

    public void RestartGame(InputAction.CallbackContext context){

        Debug.Log("load");
          SceneManager.LoadScene("Cimeterium");  

        
    }
}
