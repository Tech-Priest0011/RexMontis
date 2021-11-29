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

    private GameObject premierePlace;
    private GameObject deuxiemePlace;
    private GameObject troisiemePlace;
    private GameObject quatriemePlace;

    List<float> stockScore = new List<float>();
    private float[] listeDesScores;



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

        //Array.Sort(stockScore.ToArray());
        listeDesScores = stockScore.OrderByDescending(score => score).ToArray();
      
        foreach(float f in listeDesScores)
        {
            Debug.Log(f);
        }


        AssignationScore();
    }

    private void AssignationScore()
    {
        premierePlace.GetComponent<Text>().text = listeDesScores[0].ToString();
        deuxiemePlace.GetComponent<Text>().text = listeDesScores[1].ToString();
        troisiemePlace.GetComponent<Text>().text = listeDesScores[2].ToString();
        quatriemePlace.GetComponent<Text>().text = listeDesScores[3].ToString();

       
        
    }
}
