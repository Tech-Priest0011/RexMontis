using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    public bool inWindZone = false;
    public GameObject windZone;

    public bool inGravityHole = false;
    public GameObject gravityHole;
    public float pullForce = 1; 

/*     private Collider collider; */

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
/*             Vector3 forceDirection = transform.position - gravityHole.GetComponent<Collider>().transform.position;

            Debug.Log(forceDirection);

            rb.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime); */

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

/*             collider.GetComponent<Collider>(); */
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "windArea") {
            inWindZone = false;
        }

        if (col.gameObject.tag == "gravityHole") {
            inGravityHole = false;
        }
    }

    

}
