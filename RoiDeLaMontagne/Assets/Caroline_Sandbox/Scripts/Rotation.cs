using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
//https://docs.unity3d.com/ScriptReference/ForceMode.html?_ga=2.100837448.898169591.1634567076-594168372.1634567076

/*     public float speed;
    public float rotationSpeed;
    public GameObject gravityController; */

    /* private Rigidbody rb; */

    //With Quaternion
    /* Vector3 rotateDirection; */

    //With Torque
    /* public float torque; */


    void Start() {
        /* rb = GetComponent<Rigidbody>(); */

        //With Quaternion
        /* rotateDirection = new Vector3(0, 100, 0); */
    }

/*     void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        Vector3 movementRotation = new Vector3(verticalInput * -1, 0, horizontalInput);
        movementRotation.Normalize();
        
        if (gravityController.gameObject.GetComponent<Gravity>().isAttracting == false) {

            transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        
        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementRotation, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }
    } */

    void FixedUpdate() {
        //With Quaternion

/*         rotateDirection = new Vector3(0, Input.GetAxis("Vertical") * 100, 0);

        Quaternion rotate = Quaternion.Euler(rotateDirection * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rotate); */

        /* float turnZ = Input.GetAxis("Vertical"); */
/*         float turnY = Input.GetAxis("Vertical"); */
        /* rb.AddTorque(turnZ, 0,0); */


    }
}
