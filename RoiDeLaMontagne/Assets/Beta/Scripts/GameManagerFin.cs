using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class GameManagerFin : MonoBehaviour
{
    private float scoreFinal1;
    private float scoreFinal2;
    private float scoreFinal3;
    private float scoreFinal4;

    //
    private string player1;
    private string player2;
    private string player3;
    private string player4;
    //

    private GameObject premierePlace;
    private GameObject deuxiemePlace;
    private GameObject troisiemePlace;
    private GameObject quatriemePlace;

    List<float> stockScore = new List<float>();
    private float[] listeDesScores;

        public GameObject couronne1;
        public GameObject couronne2;
        public GameObject couronne3;
        public GameObject couronne4;

    void Start()
    {
       
        scoreFinal1 = GameManager.scoreFinal1;
        scoreFinal2 = GameManager.scoreFinal2;
        scoreFinal3 = GameManager.scoreFinal3;
        scoreFinal4 = GameManager.scoreFinal4;

        premierePlace = GameObject.Find("scoreJ1");
        deuxiemePlace = GameObject.Find("scoreJ2");
        troisiemePlace = GameObject.Find("scoreJ3");
        quatriemePlace = GameObject.Find("scoreJ4");

        stockScore.Add(scoreFinal1);
        stockScore.Add(scoreFinal2);
        stockScore.Add(scoreFinal3);
        stockScore.Add(scoreFinal4);

        //
/*         public GameObject couronne1;
        public GameObject couronne2;
        public GameObject couronne3;
        public GameObject couronne4; */
        //

        //Cette ligne classe les scores en ordre et les place dans un tableau
        //listeDesScores = stockScore.OrderByDescending(score => score).ToArray();
      
       
        AssignationScore();

        //TEST
        if (scoreFinal1 >= scoreFinal2 && scoreFinal1 >= scoreFinal3 && scoreFinal1 >= scoreFinal4) {
            couronne2.SetActive(false);
            couronne3.SetActive(false);
            couronne4.SetActive(false);

            Debug.Log("joueur 1 win");
        }

        if (scoreFinal2 >= scoreFinal1 && scoreFinal2 >= scoreFinal3 && scoreFinal2 >= scoreFinal4) {
            couronne1.SetActive(false);
            couronne3.SetActive(false);
            couronne4.SetActive(false);

            Debug.Log("joueur 2 win");
        }

        if (scoreFinal3 >= scoreFinal2 && scoreFinal3 >= scoreFinal1 && scoreFinal3 >= scoreFinal4) {
            couronne2.SetActive(false);
            couronne1.SetActive(false);
            couronne4.SetActive(false);

            Debug.Log("joueur 3 win");
        }

        if (scoreFinal4 >= scoreFinal2 && scoreFinal4 >= scoreFinal3 && scoreFinal4 >= scoreFinal1) {
            couronne2.SetActive(false);
            couronne3.SetActive(false);
            couronne1.SetActive(false);

            Debug.Log("joueur 4 win");
        }
    }


    // ===================================================================== **
    // Cette fonction assigne les scores au tableau de fin
    // ===================================================================== **
    private void AssignationScore()
    {
        premierePlace.GetComponent<Text>().text = scoreFinal1.ToString();
        deuxiemePlace.GetComponent<Text>().text = scoreFinal2.ToString();
        troisiemePlace.GetComponent<Text>().text = scoreFinal3.ToString();
        quatriemePlace.GetComponent<Text>().text = scoreFinal4.ToString(); 
    }
}
