using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // ===================================================================== **
    // Ce script est à mettre sur un personnage/joueur courant. 
    // Il modifie la position du joueur en fonction des actions des ennemis.
    // ===================================================================== **

    // Variables pour les effets de souffleuse/aspirateur
    public bool inGravityZone = false;
    public GameObject gravityZone;

    // Variables pour le lancer (pouvoir de tirer)
    public bool inThrowZone = false;
    public GameObject throwZone;
    public int throwingStrength = 10;  // Force ajoutée sur le joueur lors du lancer

    private Rigidbody rb;

    // ===================================================================== **
    // Initialisation des variables.
    // ===================================================================== **

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // ===================================================================== **
    // Détermine l'action du personnage ennemi pour influencer 
    // les mouvements du joueur.
    // ===================================================================== **

    void FixedUpdate(){
        // Se fait attirer vers l'ennemi
        if (gravityZone.GetComponent<Gravity>().isAttracting) {
            transform.position = Vector3.Lerp(transform.position, gravityZone.transform.position, Time.fixedDeltaTime*5);
        }

        // Se fait pousser par l'ennemi
        if (gravityZone.GetComponent<Gravity>().isPushing) {
            rb.AddForce(gravityZone.GetComponent<Gravity>().direction * gravityZone.GetComponent<Gravity>().strength);
        }

        // Se fait lancer/tirer par l'ennemi
        if (inThrowZone && Input.GetKey(KeyCode.V)) {
            rb.AddForce(gravityZone.GetComponent<Gravity>().direction * gravityZone.GetComponent<Gravity>().strength * throwingStrength);
        }

    }

    // ===================================================================== **
    // Détecte l'entrée du joueur dans les zones d'influence
    // des ennemis.
    // ===================================================================== **

    void OnTriggerEnter(Collider col) {
        // Zone pour pousser/attirer
        if (col.gameObject.tag == "gravityZone") {
            gravityZone = col.gameObject;
            inGravityZone = true;
        }

        // Zone pour lancer/tirer
        if (col.gameObject.tag == "throwZone") {
            throwZone = col.gameObject;
            inThrowZone = true;
        }
    }

    // ===================================================================== **
    // Détecte la sortie du joueur des zones d'influence des ennemis.
    // ===================================================================== **

    void OnTriggerExit(Collider col) {
        // Zone pour pousser/attirer
        if (col.gameObject.tag == "gravityZone") {
            inGravityZone = false;
            gravityZone = null;
        }

        // Zone pour lancer/tirer
        if (col.gameObject.tag == "throwZone") {
            inThrowZone = false;
            throwZone = null;
        }
    }
}
