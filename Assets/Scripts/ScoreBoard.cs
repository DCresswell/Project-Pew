using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    public void IncreaseScore(int plusScore)
    {
        score += plusScore;
        Debug.Log($"Current score is: {score}");
    }
}
