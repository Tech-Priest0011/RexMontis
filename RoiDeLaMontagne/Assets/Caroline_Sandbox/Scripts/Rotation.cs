using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
/*     public float speed;
    public float rotationSpeed;
    public GameObject gravityController; */

    private Rigidbody rb;
    Vector3 rotateDirection;


    void Start() {
        rb = GetComponent<Rigidbody>();

        rotateDirection = new Vector3(0, 100, 0);
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
        Quaternion rotate = Quaternion.Euler(rotateDirection * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * rotate);
    }
}
