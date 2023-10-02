using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; 

    public TextMeshProUGUI scoreText;  
    private int score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void AddScoreOnExplosion(int value)
    {
        AddScore(value); 
    }

}
