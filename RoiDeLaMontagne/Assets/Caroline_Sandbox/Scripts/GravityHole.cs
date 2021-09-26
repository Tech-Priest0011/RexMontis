using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHole : MonoBehaviour
{

    public float pullRadius = 2;
    public float pullForce = 1;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {

/*         Collider[] colliders = Physics.OverlapSphere(transform.position, pullRadius);

        foreach (var collider in colliders) 
        {
            Debug.Log(collider);

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb == null) {
                continue;
            }

            Vector3 forceDirection = transform.position - collider.transform.position;

            rb.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
        } */
    }
}
