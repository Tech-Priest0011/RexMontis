using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    //Test de code
    public ConfigurableJoint hipJoint;

    void Start()
    {
        hips = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
/*         if (Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-hips.transform.right * strafeSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.right * strafeSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward * speed);
        } */

        if(Input.GetAxis("Jump") > 0)
        {
            if(isGrounded)
            {
                hips.AddForce(new Vector3(0, jumpForce, 0));
                isGrounded = false;
            }
        }

//test
        RotateCharacter();
    }

    //test
    private void RotateCharacter() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 rotationVector = new Vector3(0, 0, 0); //
        Quaternion startRotation = hipJoint.transform.rotation;

        if (direction.magnitude >= 0.1f) {
            /* float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; */
            /* hipJoint.transform.rotation = Quaternion.Euler(0, targetAngle, 0); */
            /* hipJoint.targetRotation = Quaternion.Euler(0, targetAngle, 0); */
            hips.AddForce(direction * speed);

        if (Input.GetKey(KeyCode.W))
        {
            /* hipJoint.transform.rotation = Quaternion.Euler(0, 270, 0); */
            rotationVector = new Vector3 (0, 270, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            /* hipJoint.transform.rotation = Quaternion.Euler(0, 180, 0); */
            rotationVector = new Vector3 (0, 180, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
           /* hipJoint.transform.rotation = Quaternion.Euler(0, 0, 0); */
           rotationVector = new Vector3 (0, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            /* hipJoint.transform.rotation = Quaternion.Euler(0, 90, 0); */
            rotationVector = new Vector3 (0, 90, 0);
        }

        Debug.Log(rotationVector);

        //La ligne ci-dessous fonctionne, mais il n'y a aucune animation pour tourner/Ce n'est pas graduel.
        //hipJoint.transform.rotation = Quaternion.Euler(rotationVector);


        //Quaternion toRotation = Quaternion.LookRotation(rotationVector, Vector3.left);
        //hipJoint.transform.rotation = Quaternion.RotateTowards(hipJoint.transform.rotation, toRotation, 500 * Time.fixedDeltaTime);
           /*  Debug.Log(targetAngle); */

           hipJoint.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotationVector), 500 * Time.fixedDeltaTime);
        }
    }

}
