using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float strength;
    public Vector3 direction;

    public bool isPushing = false;
    public bool isAttracting = false;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.V)) {
            isPushing = true;

        } else {
            isPushing = false;
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            isAttracting = true;

        } else {
            isAttracting = false;
        }

        //Rotation();
        ChangeGravityDirection();
    }

    //Test pour orienter la force. La direction ne change pas avec la rotation.
    void Rotation() {
        if (Input.GetKey(KeyCode.W)) {
            Quaternion target = Quaternion.Euler(0, 270, 0);

            transform.rotation =  Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * 1f);
        }
    }

    //Test pour changer la direction de la gravite en fonction des touches directionnelles.
    //**Fonctionne**
    private void ChangeGravityDirection() {
        int gravityValue = 6;

        if (Input.GetKey(KeyCode.W)) {
            //direction
            direction = new Vector3 (0, 0, gravityValue);
        }

        if (Input.GetKey(KeyCode.D)) {
            //direction
            direction = new Vector3 (gravityValue, 0, 0);
        }

        if (Input.GetKey(KeyCode.S)) {
            //direction
            direction = new Vector3 (0, 0, -gravityValue);
        }

        if (Input.GetKey(KeyCode.A)) {
            //direction
            direction = new Vector3 (-gravityValue, 0, 0);
        }
    }
}
