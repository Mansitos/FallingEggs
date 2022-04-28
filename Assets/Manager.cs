using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject scoreManager;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject lifesUI;
    [SerializeField] int lifes;
    [SerializeField] int maxLifes;
    public float difficultyFactor = 1;

    void Start()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
    }

    public void addLife()
    {
        if (lifes < maxLifes)
        {
            lifes++;
        }

        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
    }

    public void removeLife(int lifesToRemove)
    {
        lifes = lifes - lifesToRemove;
        if(lifes <= 0)
        {
            gameOver();
        }

        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
    }

    void Update()
    {
        if (scoreManager.GetComponent<ScoreManager>().getUserScore() < 0)
        {
            gameOver();
        }


        difficultyFactor = Mathf.Clamp((100 - Mathf.Sqrt(1 + Time.realtimeSinceStartup * 5.0f)) / 100, 0.33f, 1.0f);
    }

    private void gameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.2f;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public float getDifficultyFactor()
    {
        return difficultyFactor;
    }

    public bool isMaxLife()
    {
        return lifes == maxLifes;
    }
}
