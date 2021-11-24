using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour 
{
    
    [SerializeField] public Text uiText;

    public GameObject gameManager;
    private bool timerStarted;

    public int Durer;

    private int tempsRestant;

    private bool Pause;

    public bool doublePoint = false;

    private void Start()
    {
        timerStarted = false;

    }

    private void Update() {
   

        if (gameManager.GetComponent<GameManager>().gameIsStarted && !timerStarted) { 
            Being(Durer);
            timerStarted = true;
        }

        Debug.Log("double score temps = " + doublePoint);
        VerifieTemps();
    }

    private void Being(int Second)
    {
        //Debug.Log(gameManager.GetComponent<GameManager>().gameIsStarted);
        
        
            tempsRestant = Second;
            StartCoroutine(UpdateTemps());  
        

    }

    private IEnumerator UpdateTemps()
    {

   
        while(tempsRestant >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{tempsRestant / 60:00}:{tempsRestant % 60:00}";
               
                tempsRestant--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        


        TempsTerminer();

    }

    private void VerifieTemps()
    {
        
        if (tempsRestant <= 30)
        {
            Debug.Log("SURPRISE !");
            doublePoint = true;
        }
        else
        {
            doublePoint = false;
        }
    }

    private void TempsTerminer()
    {
        //End Time , if want Do something
        print("End");
        SceneManager.LoadScene("Fin");
    }
}