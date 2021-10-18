using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
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
            
            gameObject.GetComponent<Test_Input_System>().speed = 13f;
        }
    }

    private void OnTriggerExit(Collider collision){
        gameObject.GetComponent<Test_Input_System>().speed = 100f;
    }

}
