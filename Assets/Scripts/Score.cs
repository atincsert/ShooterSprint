using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    private void Start()
    {
        currentScore = 0;
    }

    public void IncreaseScore()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void DecreaseScore()
    {
        if (currentScore <= 0)
        {
            currentScore = 0;
            return;
        }
        currentScore--;
        scoreText.text = currentScore.ToString();
    }
}
