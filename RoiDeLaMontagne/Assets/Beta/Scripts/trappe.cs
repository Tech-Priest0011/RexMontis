using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trappe : MonoBehaviour
{

    private GameObject trappes;
    public GameObject Joueur;

     //Pour le son
    public AudioClip playerDie;

    private static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "trappe")
        {
            trappes = collision.gameObject;
            trappes.GetComponent<Animator>().SetBool("close", true);
            Invoke("RemettreBoolFalse", 2);
            Joueur.GetComponent<PlayerController>().VerifieTrappe();

            //joue le son
            audioSrc.PlayOneShot(playerDie);
        }
    }




    // ===================================================================== **
    // Remet la bool�enne de l'animation de la trappe � false
    // ===================================================================== **
    private void RemettreBoolFalse()
    {
        trappes.GetComponent<Animator>().SetBool("close", false);
    }
}
