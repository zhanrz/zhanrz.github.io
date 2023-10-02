using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnCollision : MonoBehaviour
{
    public int scoreValue = 1; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ScoreManager.instance.AddScore(scoreValue);
        }
    }
}

