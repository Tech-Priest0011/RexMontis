using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{

/*     public bool inWindZone = false;
    public GameObject windZone;

    public bool inGravityHole = false;
    public GameObject gravityHole;
    public float pullForce = 1; 


    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (inWindZone) {
            rb.AddForce(windZone.GetComponent<WindArea>().direction * windZone.GetComponent<WindArea>().strength);
        }

        if (inGravityHole) {
            transform.position = Vector3.Lerp(transform.position, gravityHole.transform.position, Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "windArea") {
            windZone = col.gameObject;
            inWindZone = true;
        }

        if (col.gameObject.tag == "gravityHole") {
            gravityHole = col.gameObject;
            inGravityHole = true;

            Debug.Log(inGravityHole);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "windArea") {
            inWindZone = false;
        }

        if (col.gameObject.tag == "gravityHole") {
            inGravityHole = false;
        }
    } */

    public bool inGravityZone = false;
    public GameObject gravityZone;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        if (gravityZone.GetComponent<Gravity>().isAttracting) {
            transform.position = Vector3.Lerp(transform.position, gravityZone.transform.position, Time.fixedDeltaTime);
        }

        if (gravityZone.GetComponent<Gravity>().isPushing) {
            rb.AddForce(gravityZone.GetComponent<Gravity>().direction * gravityZone.GetComponent<Gravity>().strength);
        }

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "gravityZone") {
            gravityZone = col.gameObject;
            inGravityZone = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "gravityZone") {
            inGravityZone = false;
            gravityZone = null;
        }
    }

    

}
