using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trappe : MonoBehaviour
{

    private GameObject trappes;
    public GameObject Joueur;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "trappe")
        {
            trappes = collision.gameObject;
            trappes.GetComponent<Animator>().SetBool("close", true);
            Invoke("RemettreBoolFalse", 2);
            Joueur.GetComponent<PlayerController>().VerifieTrappe();
        }
    }




    // ===================================================================== **
    // Remet la booléenne de l'animation de la trappe à false
    // ===================================================================== **
    private void RemettreBoolFalse()
    {
        trappes.GetComponent<Animator>().SetBool("close", false);
    }
}
