using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEffects : MonoBehaviour
{
    //
    public bool inWindArea = false;
    public GameObject windArea;
    //

/*     private transform positionY; */

    public CharacterController controller;


    //Fonctionne sur les gameObject qui n'ont pas de character controller
   /*  private Rigidbody rb; */

    void Start() {
        //Fonctionne sur les gameObject qui n'ont pas de character controller
/*         rb = GetComponent<Rigidbody>(); */

        controller = GetComponent<CharacterController>();


/*         positionY = transform.position.y; */
    }

    void FixedUpdate(){

        //Fonctionne sur les gameObject qui n'ont pas de character controller
/*         if (inWindArea) {
            rb.AddForce(windArea.GetComponent<WindArea>().direction * windArea.GetComponent<WindArea>().strength);
        } */

        

/*         Vector3 velo = controller.velocity;
        velo = new Vector3(controller.velocity.x, controller.velocity.y * -9.81f, controller.velocity.z); */

        if (inWindArea) {
            //Fonctionne, mais le personnage reste dans les airs. Il ne tombe pas.
            /* transform.Translate(Vector3.up * Time.fixedDeltaTime); */

        }



    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "windArea") {
            windArea = col.gameObject;
            inWindArea = true;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "windArea") {
            inWindArea = false;
            windArea = null;
        }
    }
}
