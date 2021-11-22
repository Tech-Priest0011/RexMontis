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

    private void Start()
    {
        timerStarted = false;
    }

    private void Update() {
        if (gameManager.GetComponent<GameManager>().gameIsStarted && !timerStarted) { 
            Being(Durer);
            timerStarted = true;
        }
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

    private void TempsTerminer()
    {
        //End Time , if want Do something
        print("End");
        SceneManager.LoadScene("Fin");
    }
}