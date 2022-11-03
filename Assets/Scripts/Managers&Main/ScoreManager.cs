using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreUI;
    private int userScoreInSession = 0;

    void Start()
    {
        scoreUI = this.GetComponent<Text>();
        scoreUI.text = userScoreInSession.ToString();
    }

    void Update()
    {
        scoreUI.text = userScoreInSession.ToString();
    }

    public void addScore(int value)
    {
        userScoreInSession = userScoreInSession + value;
    }

    public void removeScore(int value)
    {
        userScoreInSession = userScoreInSession - value;
    }

    public int getUserScore()
    {
        return userScoreInSession;
    }
}
