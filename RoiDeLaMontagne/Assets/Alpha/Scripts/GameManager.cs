using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //Variables pour le temps
    private float tempsDejeu = 60f;
    private float tempsDeDepart;
    static private float tempsFinal = 0f;
    private float interval = 2;

    //Variables pour le score
    private int score;
    static private string pointageFinal;
    private float hauteurTemplate = 60f;
    private List<tableScores> listeDesScores;
    private List<Transform> listeDesTransformDesScores;
    public GameObject tableauDesScores;

    //Variables non-class�es
    public GameObject character;
    private string scene;
    static private string nomDuJoueur;

    [Header("UI Elements")]

    public Text champsTemps;
    public Text champsScore;
    public Text champsNomEntre;
    public Text champsNom;
    public Transform scoreContainer;
    public Transform scoreTemplate;



    private void AjouterScore(tableScores tableDesScores, Transform container, List<Transform> listeDeTransform)
    {
        Transform scoreTransform = Instantiate(scoreTemplate, container);
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
        scoreRectTransform.anchoredPosition = new Vector2(hauteurTemplate * listeDeTransform.Count, 0 );
        scoreTransform.gameObject.SetActive(true);

        int rang = listeDeTransform.Count + 1;
        //Debug.Log(rang);
        string rangString;

        switch (rang)
        {
            default: rangString = rang + "e"; break;
            case 1: rangString = "1er"; break;

        }
        scoreTransform.Find("positionText").GetComponent<Text>().text = rangString;

        int scoreText = tableDesScores.scoreUnique;
        scoreTransform.Find("nomsText").GetComponent<Text>().text = scoreText.ToString();

        string nomsText = tableDesScores.nomUnique;
        scoreTransform.Find("scoreText").GetComponent<Text>().text = nomsText;
        
        
        //  if(listeDeTransform.Count >= 4)
        //  {
        //        listeDeTransform[1].GetChild(1).GetComponent<RectTransform>().active = false;
        //  }

        listeDeTransform.Add(scoreTransform);
    }

    //Repr�sente un seul score et un seul nom
    private class tableScores   
    {
        public int scoreUnique;
        public string nomUnique;

    }

    void Start()
    {
        scoreTemplate.gameObject.SetActive(false);

        listeDesScores = new List<tableScores>()
        {
            new tableScores { scoreUnique = 5899, nomUnique = "Max" },
            new tableScores { scoreUnique = 8000, nomUnique = "Caro" },
            new tableScores { scoreUnique = 9899, nomUnique = "Gab" },
            new tableScores { scoreUnique = 7899, nomUnique = "Jerry" },
            new tableScores { scoreUnique = 6899, nomUnique = "J�" },
            new tableScores { scoreUnique = 2899, nomUnique = "Sam" },
            new tableScores { scoreUnique = 4899, nomUnique = "Mik" },
            new tableScores { scoreUnique = 3899, nomUnique = "Rudy" },
            
        };

        //Met les scores dans le bon ordre
        for (int i = 0; i < listeDesScores.Count; i++)
        {
            for (int j = i + 1; j < listeDesScores.Count; j++)
            {
                if(listeDesScores[j].scoreUnique > listeDesScores[i].scoreUnique)
                {
                    //Interchange les positions
                    tableScores temporaire = listeDesScores[i];
                    listeDesScores[i] = listeDesScores[j];
                    listeDesScores[j] = temporaire;
                }
            }
        }
        listeDesTransformDesScores = new List<Transform>();
        foreach(tableScores tableDesScores in listeDesScores)
        {
            AjouterScore(tableDesScores, scoreContainer, listeDesTransformDesScores);
        }
       
       /////////////////////////////////////////////////////////////////////////////////////Awake en haut
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
            // if (tempsDejeu <= 99f && tempsDejeu > 9f)
            // {
            //     champsTemps.text = "00:" + Mathf.Ceil(tempsDejeu);

            // }else if(tempsDejeu <= 9f)
            // {
            //     champsTemps.text = "00:0" + Mathf.Ceil(tempsDejeu);
            // }


            // tempsDejeu -= 1 * Time.deltaTime;

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

            if (Input.GetKeyDown(KeyCode.F1) && tableauDesScores.activeSelf == true)
            {
                tableauDesScores.SetActive(false);
            }
            else if(Input.GetKeyDown(KeyCode.F1) && tableauDesScores.activeSelf == false)
            {
                tableauDesScores.SetActive(true);
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
