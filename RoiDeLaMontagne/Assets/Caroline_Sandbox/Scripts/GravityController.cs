using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public bool inGravityZone = false;
    public GameObject gravityZone;

    //
    public bool inThrowZone = false;
    public GameObject throwZone;
    public int throwingStrength = 10;
    //

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        if (gravityZone.GetComponent<Gravity>().isAttracting) {
            transform.position = Vector3.Lerp(transform.position, gravityZone.transform.position, Time.fixedDeltaTime*5);
        }

        if (gravityZone.GetComponent<Gravity>().isPushing) {
            rb.AddForce(gravityZone.GetComponent<Gravity>().direction * gravityZone.GetComponent<Gravity>().strength);
        }

        if (inThrowZone && Input.GetKey(KeyCode.V)) {
            rb.AddForce(gravityZone.GetComponent<Gravity>().direction * gravityZone.GetComponent<Gravity>().strength * throwingStrength);
        }

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "gravityZone") {
            gravityZone = col.gameObject;
            inGravityZone = true;
        }

        if (col.gameObject.tag == "throwZone") {
            throwZone = col.gameObject;
            inThrowZone = true;
            Debug.Log(inThrowZone);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "gravityZone") {
            inGravityZone = false;
            gravityZone = null;
        }

        if (col.gameObject.tag == "throwZone") {
            inThrowZone = false;
            Debug.Log(inThrowZone);
            throwZone = null;
        }
    }

    

}
