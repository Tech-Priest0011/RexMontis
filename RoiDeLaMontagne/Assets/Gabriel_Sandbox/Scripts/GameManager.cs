using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private float tempsDejeu = 15f;
    private float tempsDepart;
    static public float tempsFinal = 0f;
    public GameObject character;
    private int score;

  
    [Header("UI Elements")]

    public Text champsTemps;
    public Text champsScore;


    void Start()
    {
        tempsDepart = tempsDejeu; 
    }

    
    void Update()
    {
        Decompte();
        Score();
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

    public void Score()
    {
        score = character.GetComponent<Character>().scorePerso;
        if(score >= 1000)
        {
            champsScore.text = "Score : " + score;
        }
        else if (score >= 100)
        {
            champsScore.text = "Score : 0" + score;
        }
        else if (score >= 10)
        {
            champsScore.text = "Score : 00" + score;
        }
        else if(score >= 0)
        {
             champsScore.text = "Score : 000" + score;
        }
             
    }

    











































}
