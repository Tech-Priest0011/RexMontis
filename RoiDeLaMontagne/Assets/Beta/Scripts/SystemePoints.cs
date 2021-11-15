using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemePoints : MonoBehaviour
{
    private GameManager scoreManager;
    public float areaPoints;
    private float defaultPoints = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType <GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter (Collider collider){

                if (collider.gameObject.tag == "Player")
                {
                scoreManager.scoreBonus1 = areaPoints;
                scoreManager.scoreBonus2 = areaPoints;
                scoreManager.scoreBonus3 = areaPoints;
                scoreManager.scoreBonus4 = areaPoints;
                scoreManager.scoreBonus5 = areaPoints;
                scoreManager.scoreBonus6 = areaPoints;
                scoreManager.scoreBonus7 = areaPoints;
                scoreManager.scoreBonus8 = areaPoints;
                }
    }
    
    void OnTriggerExit(Collider collider){
        if (collider.gameObject.tag == "Player")
        {
            scoreManager.scoreBonus1 = defaultPoints;
               scoreManager.scoreBonus2 = defaultPoints;
                scoreManager.scoreBonus3 = defaultPoints;
                scoreManager.scoreBonus4 = defaultPoints;
                scoreManager.scoreBonus5 = defaultPoints;
                scoreManager.scoreBonus6 = defaultPoints;
                scoreManager.scoreBonus7 = defaultPoints;
                scoreManager.scoreBonus8 = defaultPoints;
        }
    }
}
