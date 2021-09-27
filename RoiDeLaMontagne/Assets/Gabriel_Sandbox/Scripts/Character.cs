using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public int scorePerso = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }


    private void Score()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scorePerso += 10;
            //Debug.Log(scorePerso);
        }
    }
}
