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

        Rotation();
    }

    void Rotation() {
        if (Input.GetKey(KeyCode.W)) {
            Quaternion target = Quaternion.Euler(0, 270, 0);

            transform.rotation =  Quaternion.Slerp(transform.rotation, target, Time.fixedDeltaTime * 1f);

             /* transform.rotation = new Vector3 (0, 270, 0); */
        }
    }
}
