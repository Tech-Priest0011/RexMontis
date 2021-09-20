using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personnage : MonoBehaviour
{

    public bool inWindZone = false;
    public GameObject windZone;

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
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "windArea") {
            windZone = col.gameObject;
            inWindZone = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "windArea") {
            inWindZone = false;
        }
    }

}
