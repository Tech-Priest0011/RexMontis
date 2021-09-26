using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float tempsDejeu = 15f;
    private float tempsDepart;
    static public float tempsFinal = 0f;
    public Text champsTemps;


    void Start()
    {
        tempsDepart = tempsDejeu;
    }

    
    void Update()
    {
        Decompte();
    }


    public void Decompte()
    {
        if (tempsDejeu <= 99f && tempsDejeu > 9f)
        {
            champsTemps.text = "00:" + Mathf.Ceil(tempsDejeu);

        }else if(tempsDejeu <= 9f)
        {
            champsTemps.text = "00:0" + Mathf.Ceil(tempsDejeu);
        }


        tempsDejeu -= 1 * Time.deltaTime;

        if (tempsDejeu <= 0)
        {
            tempsDejeu = 0;
            Debug.Log("fin du jeu");


        }
    }















































}
