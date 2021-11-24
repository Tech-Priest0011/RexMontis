using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Variables pour le temps
    private int tempsDejeu;
    private float tempsDeDepart;
    static private float tempsFinal = 0f;
    private float interval = 2;

    //Variables pour le score
    static private string pointageFinal;
    private float hauteurTemplate = 60f;
    private List<tableScores> listeDesScores;
    private List<Transform> listeDesTransformDesScores;
    public GameObject tableauDesScores;
    public float pointIncreasedPerSecond;
    
    private float multiplicateur = 1f;

    //Variables non-class�es
    public GameObject character;
    private string scene;
    static private string nomDuJoueur;

    private string quelleScene;

    [Header("UI Elements")]

    public Text champsTemps;
    public Text champsScore;
    public Text champsNomEntre;
    public Text champsNom;
    public Transform scoreContainer;
    public Transform scoreTemplate;

    //Variables pour la connexion des joueurs
    public GameObject joueurConnecte;
    public GameObject parentJoueurConnecte;
    private int nombreJoueur = 0;

    //Test pour l'accueil
    public bool gameIsStarted;

    // Score Points 
    public float scoreJoueur1 = 0;
    public float scoreJoueur2 = 0;
    public float scoreJoueur3 = 0;
    public float scoreJoueur4 = 0;
    public float scoreJoueur5 = 0;
    public float scoreJoueur6 = 0;
    public float scoreJoueur7 = 0;
    public float scoreJoueur8 = 0; 
    public List<tableData> CounterObjects = new List<tableData>();
    public struct tableData{
    public Transform scoreTransform;
    public tableScores Score;
    }
    public float scoreBonus1 = 11f;
    public float scoreBonus2 = 11f;
    public float scoreBonus3 = 12f;
    public float scoreBonus4 = 13f;
    public float scoreBonus5 = 14f;
    public float scoreBonus6 = 15f;
    public float scoreBonus7 = 16f;
    public float scoreBonus8 = 17f;

    public List<GameObject> Players {get; set;}
    public static int playerID = 0;
    public static Dictionary<GameObject,int> playerList = new Dictionary<GameObject,int>();




    // ===================================================================== **
    // Cette fonction est appelé au tout début de la partie
    // ===================================================================== **
    void Start()
    {
        gameIsStarted = false;

        listeDesScores = new List<tableScores>()
        {
            new tableScores { scoreUnique = scoreJoueur1, nomUnique = "Max" },
            new tableScores { scoreUnique = scoreJoueur2, nomUnique = "Caro" },
            new tableScores { scoreUnique = scoreJoueur3, nomUnique = "Gab" },
            new tableScores { scoreUnique = scoreJoueur4, nomUnique = "Jerry" },
            new tableScores { scoreUnique = scoreJoueur5, nomUnique = "J�" },
            new tableScores { scoreUnique = scoreJoueur6, nomUnique = "Sam" },
            new tableScores { scoreUnique = scoreJoueur7, nomUnique = "Mik" },
            new tableScores { scoreUnique = scoreJoueur8, nomUnique = "Rudy" },
        };
        
        listeDesTransformDesScores = new List<Transform>();
            foreach(tableScores tableDesScores in listeDesScores)
            {
             AjouterScore(tableDesScores, scoreContainer, listeDesTransformDesScores);
            }
  
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

        Players = new List<GameObject>();
    }

    // ===================================================================== **
    // Cette fonction est appelé à chaque frame
    // ===================================================================== **
    void Update()
    {
        

        quelleScene = SceneManager.GetActiveScene().name;
        tempsDejeu = GameObject.Find("temps").GetComponent<CountDown>().tempsRestant;
      
        scoreTemplate.gameObject.SetActive(false);
        CounterObjects[0].Score.scoreUnique = scoreJoueur1;
        CounterObjects[1].Score.scoreUnique = scoreJoueur2;
        CounterObjects[2].Score.scoreUnique = scoreJoueur3;
        CounterObjects[3].Score.scoreUnique = scoreJoueur4;
        CounterObjects[4].Score.scoreUnique = scoreJoueur5;
        CounterObjects[5].Score.scoreUnique = scoreJoueur6;
        CounterObjects[6].Score.scoreUnique = scoreJoueur7;
        CounterObjects[7].Score.scoreUnique = scoreJoueur8;

       foreach(tableData data in CounterObjects)
       {
        
        float scoreText = data.Score.scoreUnique;
        data.scoreTransform.Find("nomsText").GetComponent<Text>().text = scoreText.ToString();

        string nomsText = data.Score.nomUnique;
        data.scoreTransform.Find("scoreText").GetComponent<Text>().text = nomsText;
       }

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

        //Test début du jeu
        if (gameIsStarted) {
           
            Score();
        }
        
    }

    // ===================================================================== **
    // Cette fonction est ajoute un bonus au score du joueur
    // ===================================================================== **
    public void setBonusScore(float score, int index)
    {
        listeDesScores[index].bonusScore = score;

    }

    // ===================================================================== **
    // Cette fonction est ajoute un ID au joueur qui se connecte
    // ===================================================================== **
    public static void addPlayerID(GameObject obj)
    {
        playerList.Add(obj, playerID);
        playerID++;
    }


    // ===================================================================== **
    // Cette fonction est ajoute un score à chaque joueur et l'affiche sur la scène
    // ===================================================================== **
    private void AjouterScore(tableScores tableDesScores, Transform container, List<Transform> listeDeTransform)
    {
        Transform scoreTransform = Instantiate(scoreTemplate, container); // Crée un clone dans un container
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>(); // Va chercher la position du clone 
        scoreRectTransform.anchoredPosition = new Vector2(hauteurTemplate * listeDeTransform.Count, 0 );  //Descend le score cloné d'une certaine hauteur
        scoreTransform.gameObject.SetActive(true); // Active le clone 
        tableData data = new tableData();
        data.scoreTransform = scoreTransform;
        data.Score = tableDesScores;
        CounterObjects.Add(data);
        
        // --- Crée les positions --- //
        int rang = listeDeTransform.Count + 1;
        string rangString;
        switch (rang)
        {
            default: rangString = rang + "e"; break;
            case 1: rangString = "1er"; break;
        }

        scoreTransform.Find("positionText").GetComponent<Text>().text = rangString;

        float scoreText = tableDesScores.scoreUnique;
        scoreTransform.Find("nomsText").GetComponent<Text>().text = scoreText.ToString();

        string nomsText = tableDesScores.nomUnique;
        scoreTransform.Find("scoreText").GetComponent<Text>().text = nomsText;
    
        listeDeTransform.Add(scoreTransform);
    }

    // ===================================================================== **
    // Cette fonction représente un score et un nom pour chaque joueur
    // ===================================================================== **
    public class tableScores   
    {
        public float scoreUnique;
        public string nomUnique;
        public float bonusScore = 10;
    }

    // ===================================================================== **
    // Cette fonction gère l'augmentation des scores avec le temps
    // ===================================================================== **
    public void Score()
    {
        if(scene != "Fin")
        {
            interval -= 1 * Time.deltaTime;
            
            if(interval <= 0){
       
                interval += 2f;

                if (tempsDejeu >= 30)
                {
                    scoreJoueur1 += listeDesScores[0].bonusScore;
                    scoreJoueur2 += listeDesScores[1].bonusScore;
                    scoreJoueur3 += listeDesScores[2].bonusScore;
                    scoreJoueur4 += listeDesScores[3].bonusScore;
                    scoreJoueur5 += listeDesScores[4].bonusScore;
                    scoreJoueur6 += listeDesScores[5].bonusScore;
                    scoreJoueur7 += listeDesScores[6].bonusScore;
                    scoreJoueur8 += listeDesScores[7].bonusScore;
                }
                else
                {
                    multiplicateur = 2;
                    scoreJoueur1 += listeDesScores[0].bonusScore * multiplicateur;
                    scoreJoueur2 += listeDesScores[1].bonusScore * multiplicateur;
                    scoreJoueur3 += listeDesScores[2].bonusScore * multiplicateur;
                    scoreJoueur4 += listeDesScores[3].bonusScore * multiplicateur;
                    scoreJoueur5 += listeDesScores[4].bonusScore * multiplicateur;
                    scoreJoueur6 += listeDesScores[5].bonusScore * multiplicateur;
                    scoreJoueur7 += listeDesScores[6].bonusScore * multiplicateur;
                    scoreJoueur8 += listeDesScores[7].bonusScore * multiplicateur;
                } 
            }
        } 
    }


    // ===================================================================== **
    // Cette fonction connecte un joueur lorsqu'un joueur appuies sur X
    // ===================================================================== **
    public void connexionJoueur()
    {
        nombreJoueur++;
        joueurConnecte.GetComponent<Text>().text = "Joueur " + nombreJoueur.ToString();
        Instantiate(joueurConnecte, parentJoueurConnecte.transform); 
    }


    // ===================================================================== **
    // Cette fonction démarre la partie
    // ===================================================================== **
    public void Jouer(InputAction.CallbackContext context){

        if (nombreJoueur >= 2) {
            gameIsStarted = true;
            GameObject.Find("Instructions").SetActive(false);
        }
    }


    // ===================================================================== **
    // Cette fonction amène vers la scène de fin et garde le texte en mémoire
    // ===================================================================== **
    public void FinDeJeu()
    {
        SceneManager.LoadScene("Fin");
        pointageFinal = champsScore.text;
        gameIsStarted = false;
        
    }

    // ===================================================================== **
    // Cette fonction redémarre la partie
    // ===================================================================== **
    public void Restart(InputAction.CallbackContext context){

        if(quelleScene == "Fin"){
            SceneManager.LoadScene("Cimeterium");
        }
        
    }

}