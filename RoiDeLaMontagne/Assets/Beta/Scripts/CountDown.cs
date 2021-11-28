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

    public int tempsRestant;

    private bool Pause;

    public bool doublePoint = false;

    


    // ===================================================================== **
    // Cette fonction est appel� au d�but de la partie
    // ===================================================================== **
    private void Start()
    {
        timerStarted = false;
  
    }

    // ===================================================================== **
    // Cette fonction est appel� � chaque frame
    // ===================================================================== **
    private void Update() {
        if (gameManager.GetComponent<GameManager>().gameIsStarted && !timerStarted) { 
            Being(Durer);
            timerStarted = true;
        }

/*         if (tempsRestant <= 0 && timerStarted) {
            GameObject.Find("Countdown").GetComponent<Text>().text = "C'est fini!";
        } */
    }

    // ===================================================================== **
    // Cette fonction cette fonction d�marre la couroutine UpdateTemps
    // ===================================================================== **
    private void Being(int Second)
    {
            tempsRestant = Second;
            StartCoroutine(UpdateTemps());  
    }


    // ===================================================================== **
    // Cette fonction actualise le temps � chaque seconde 
    // ===================================================================== **
    private IEnumerator UpdateTemps()
    {
        while(tempsRestant >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{tempsRestant / 60:00}:{tempsRestant % 60:00}";
               
                tempsRestant --;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        TempsTerminer();
    }

   

    // ===================================================================== **
    // Cette fonction charge la sc�ne de fin quand le temps atteint z�ro
    // ===================================================================== **
    private void TempsTerminer()
    {
        //End Time , if want Do something
        SceneManager.LoadScene("Fin");
      
    }
}