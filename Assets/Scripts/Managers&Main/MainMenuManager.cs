using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private SceneNavigator sceneNavigator;

    [SerializeField] GameObject recordText;
    [SerializeField] GameObject coinBalanceText;

    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject optionsPage;
    [SerializeField] GameObject aboutPage;
    [SerializeField] GameObject shopPage;

    private LoadAndSaveSystem loadAndSaveSystem;

    void Start()
    {
        initializeComponents();
        updateRecordScoreText();
        updateCoinBalanceText();
    }

    private void initializeComponents()
    {
        sceneNavigator = GameObject.FindGameObjectWithTag("SceneNavigator").GetComponent<SceneNavigator>();
        loadAndSaveSystem = GameObject.FindGameObjectWithTag("LoadAndSaveSystem").GetComponent<LoadAndSaveSystem>();
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

    private void disableAllPages()
    {
        mainPage.SetActive(false);
        optionsPage.SetActive(false);
        aboutPage.SetActive(false);
        shopPage.SetActive(false);
    }

    public void loadMainPage()
    {
        disableAllPages();
        mainPage.SetActive(true);
    }

    public void loadShopPage()
    {
        disableAllPages();
        shopPage.SetActive(true);
    }

    public void loadAboutPage()
    {
        disableAllPages();
        aboutPage.SetActive(true);
    }

    public void loadOptionsPage()
    {
        disableAllPages();
        optionsPage.SetActive(true);
    }

}
