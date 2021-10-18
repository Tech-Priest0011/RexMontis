using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Collider colliderPerso;
    private float speed;

    void Start()
    {
        speed = gameObject.GetComponent<Test_Input_System>().speed;

    }

    void Update()
    {
        Debug.Log(speed);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Water")
        {
            Debug.Log("collision");
            
        }
    }

}
