using System;
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
    [SerializeField] GameObject mainCamera;
    [SerializeField] int lifes;
    [SerializeField] int maxLifes;
    [SerializeField] bool devMode;
    [ShowOnly] [SerializeField] int collectedCoins = 0;
    

    private CameraShake cameraShake;
    private LoadAndSaveSystem loadAndSaveSystem;
    private SceneNavigator sceneNavigator;

    private UserData userData;

    void Start()
    {
        Time.timeScale = 1;
        gameOverUI.SetActive(false);
        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
        cameraShake = mainCamera.GetComponent<CameraShake>();
        loadAndSaveSystem = GameObject.FindGameObjectWithTag("LoadAndSaveSystem").GetComponent<LoadAndSaveSystem>();
        sceneNavigator = GameObject.FindGameObjectWithTag("SceneNavigator").GetComponent<SceneNavigator>();
        userData = loadAndSaveSystem.loadUserData();
}

    public void addLife()
    {
        if (lifes < maxLifes)
        {
            lifes++;
        }

        updateUI();
    }

    public void removeLife(int lifesToRemove)
    {
        lifes = lifes - lifesToRemove;
        if(lifes <= 0 && !devMode)
        {
            gameOver();
        }

        updateUI();
        StartCoroutine(cameraShake.Shake(0.16f, 0.18f));
    }

    private void updateUI()
    {
        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
    }

    void Update()
    {
    }

    public void gameOver()
    {
        int score = scoreManager.GetComponent<ScoreManager>().getUserScore();
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<GameOverUI>().updateGameOverUI(score, userData.scoreRecord);
        Time.timeScale = 0.2f;

        registerScoreOnUserData();
    }

    private void registerScoreOnUserData()
    {
        int score = scoreManager.GetComponent<ScoreManager>().getUserScore();
        if (userData.scoreRecord < score)
        {
            Debug.Log("Saving new user record!");
            userData.scoreRecord = score;
        }

        userData.coinBalance += collectedCoins;
        collectedCoins = 0;

        loadAndSaveSystem.saveUserData(userData);

    }

    public void restartGame()
    {
        sceneNavigator.loadPlayScene();
    }

    public float getDifficultyFactor(float initTimeOffset)
    {
        return Mathf.Clamp((100 - Mathf.Sqrt(1 + (Time.realtimeSinceStartup - initTimeOffset) * 5.0f)) / 100, 0.33f, 1.0f);
    }

    public bool isMaxLife()
    {
        return lifes == maxLifes;
    }

    public void collectCoin(int value)
    {
        collectedCoins += value;
    }

    public void setLifes(int value)
    {
        lifes = 0;
        updateUI();
    }

    public void backToMainMenu()
    {
        sceneNavigator.loadMenuScene();
    }
}
