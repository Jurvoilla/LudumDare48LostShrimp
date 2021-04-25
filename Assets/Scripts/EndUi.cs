using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUi : MonoBehaviour
{
    int score;
    public GameObject PanelScore;
    public Text scoreText;
    public Text bestScoreText;

    
    void Start()
    {
        PanelScore.SetActive(false);
        scoreText.text = "Score : " + score + " meters";
        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore", score) + "meters";
        transform.localScale = Vector3.zero;
    }
    void OnEnable()
    {
        StartCoroutine(Pop());
    }

    IEnumerator Pop()
    {
        float time = 0f;
        while(time < 0.1f)
        {
            transform.localScale = new Vector3(time/0.1f, time/0.1f, time/0.1f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    public void SetScore(int _score)
    {
        this.score = _score;
    }
}
