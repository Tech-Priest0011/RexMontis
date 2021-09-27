using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Variables pour le temps
    private float tempsDejeu = 15f;
    private float tempsDepart;
    static public float tempsFinal = 0f;


    public GameObject character;
    private int score;
    private string scene;
    static private string nomDuJoueur;

    private float interval = 2;

  
    [Header("UI Elements")]

    public Text champsTemps;
    public Text champsScore;
    public Text champsNomEntre;
    public Text champsNom;


    void Start()
    {
        tempsDepart = tempsDejeu; 
        scene = SceneManager.GetActiveScene().name;

        if(scene == "Scene1"){
            champsNom.text = nomDuJoueur;
            
        }
    }

    
    void Update()
    {
        Decompte();
        Score();
        
    }

  

   


    public void Decompte()
    {   
        if(scene != "Intro"){
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

    public void Score()
    {
        if(scene != "Intro"){
            //score = character.GetComponent<Character>().scorePerso;

            interval -= 1 * Time.deltaTime;
            if(interval <= 0){
                interval += 2;
                score += 10;
            }
            

            


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

    


    public void Jouer(){
        SceneManager.LoadScene("Scene1");
        nomDuJoueur =champsNomEntre.text;
    }
    











































}
