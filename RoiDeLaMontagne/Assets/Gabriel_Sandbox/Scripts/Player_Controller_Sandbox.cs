using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Sandbox : MonoBehaviour
{
    public Collider colliderPerso;
    

    void Start()
    {
        //speed = gameObject.GetComponent<Test_Input_System>().speed;

    }

    void Update()
    {
       

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Water")
        {
            
            gameObject.GetComponent<Test_Input_System>().speed = 5f;

        }
    }

    private void OnTriggerExit(Collider collision){
        gameObject.GetComponent<Test_Input_System>().speed = 10f;
    }

}
