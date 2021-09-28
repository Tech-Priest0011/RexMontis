using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Variables pour le temps
    private float tempsDejeu = 3f;
    private float tempsDeDepart;
    static private float tempsFinal = 0f;
    static private string pointageFinal;


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
        scene = SceneManager.GetActiveScene().name;

        if (scene == "Scene1"){
            champsNom.text = nomDuJoueur;
            if (champsNom.text == "")
            {
                champsNom.text = "Joueur";
            }
            tempsDeDepart = tempsDejeu;

        }else if (scene == "Fin")
        {
            champsNom.text = nomDuJoueur;
            if (champsNom.text == "")
            {
                champsNom.text = "Joueur";
            }
            champsTemps.text = tempsFinal.ToString();
            champsScore.text = pointageFinal;
            
        }
    }

    
    void Update()
    {
        Decompte();
        Score();
        
    }

  

   


    public void Decompte()
    {   
        if(scene != "Intro" && scene != "Fin")
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
                FinDeJeu();


            }
        }
        
    }

    public void Score()
    {
        if(scene != "Intro" && scene != "Fin")
        {
            //score = character.GetComponent<Character>().scorePerso;

            interval -= 1 * Time.deltaTime;
            if(interval <= 0){
                interval += 2;

                if(tempsDejeu >= 10){
                    score += 10;
                }else{
                    score += 30;
                }
                
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
        nomDuJoueur = champsNomEntre.text;

    }

    public void FinDeJeu()
    {
        SceneManager.LoadScene("Fin");
        tempsFinal = tempsDeDepart - tempsDejeu;
        pointageFinal = champsScore.text;
        
    }


    











































}
