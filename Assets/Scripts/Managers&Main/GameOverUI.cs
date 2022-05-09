using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject newRecordText;

    void Start()
    {
    }

    void Update()
    {
    }

    public void updateGameOverUI(int newScore, int record)
    {
        if(newScore > record)
        {
            scoreText.SetActive(false);
            newRecordText.SetActive(true);

            newRecordText.GetComponent<Text>().text = "New Record!\n" + newScore.ToString();
        }
        else
        {
            scoreText.SetActive(true);
            newRecordText.SetActive(false);

            scoreText.GetComponent<Text>().text = "Your score: " + newScore.ToString() + "\nYour record: " + record.ToString();
        }
    }

}
