using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public Transform profondeur;
    public Text pronfTexf;
    public Text bestTexf;
    public EndUi endUi;


    void Start()
    {
        bestTexf.text = "Best : " + PlayerPrefs.GetInt("BestScore", 0) + "m";
    }

    private void FixedUpdate() 
    {
        pronfTexf.text = ((int)-profondeur.position.y).ToString() + " m" ;
    }


    public void PlayerDie()
    {
        int score = ((int)-profondeur.position.y);
        int best = PlayerPrefs.GetInt("BestScore", 0);
        if(score > best)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
        endUi.SetScore(score);
        endUi.gameObject.SetActive(true);
    }
}
