using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private SceneNavigator sceneNavigator;
    [SerializeField] GameObject recordText;
    [SerializeField] GameObject coinBalanceText;

    private LoadAndSaveSystem loadAndSaveSystem;

    void Start()
    {
        sceneNavigator = GameObject.FindGameObjectWithTag("SceneNavigator").GetComponent<SceneNavigator>();
        loadAndSaveSystem = GameObject.FindGameObjectWithTag("LoadAndSaveSystem").GetComponent<LoadAndSaveSystem>();

        updateRecordScoreText();
        updateCoinBalanceText();
    
    }

    private void updateCoinBalanceText()
    {
        UserData userData = loadAndSaveSystem.loadUserData();
        coinBalanceText.GetComponent<Text>().text = "Coins: " + userData.coinBalance.ToString();
    }

    private void updateRecordScoreText()
    {
        UserData userData = loadAndSaveSystem.loadUserData();
        recordText.GetComponent<Text>().text = "Record: " + userData.scoreRecord.ToString();
    }

    public void play()
    {
        sceneNavigator.loadPlayScene();
    }
}
