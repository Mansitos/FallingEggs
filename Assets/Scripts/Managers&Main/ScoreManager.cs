using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private Text scoreUI;
    private int userScore = 0;

    void Start()
    {
        scoreUI = this.GetComponent<Text>();
        scoreUI.text = userScore.ToString();
    }

    void Update()
    {
        scoreUI.text = userScore.ToString();
    }

    public void addScore(int value)
    {
        userScore = userScore + value;
    }

    public void removeScore(int value)
    {
        userScore = userScore - value;
    }

    public int getUserScore()
    {
        return userScore;
    }
}
