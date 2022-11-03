using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject scoreManager;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject lifesUI;
    [SerializeField] GameObject mainCamera;
    [SerializeField] int lifes;
    [SerializeField] int maxLifes;
    [SerializeField] public bool devMode;
    [ShowOnly] [SerializeField] int collectedCoinInSession = 0;
    

    private CameraShake cameraShake;
    private LoadAndSaveSystem loadAndSaveSystem;
    private SceneNavigator sceneNavigator;

    private UserData userData;

    void Start()
    {
        // Initialization
        Time.timeScale = 1;
        initializeUI();
        initializeComponents();
        
        // Load user data
        userData = loadAndSaveSystem.loadUserData();
}

    private void initializeComponents()
    {
        cameraShake = mainCamera.GetComponent<CameraShake>();
        loadAndSaveSystem = GameObject.FindGameObjectWithTag("LoadAndSaveSystem").GetComponent<LoadAndSaveSystem>();
        sceneNavigator = GameObject.FindGameObjectWithTag("SceneNavigator").GetComponent<SceneNavigator>();
    }

    private void initializeUI()
    {
        gameOverUI.SetActive(false);
        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
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
            handleGameOver();
        }

        updateUI();
        StartCoroutine(cameraShake.Shake(0.16f, 0.18f));
    }

    private void updateUI()
    {
        lifesUI.GetComponentInChildren<Text>().text = lifes.ToString();
    }

    public void handleGameOver()
    {
        int reachedScoreInSession = scoreManager.GetComponent<ScoreManager>().getUserScore();
        Time.timeScale = 0.2f;

        // Update GameOverUI
        gameOverUI.SetActive(true);
        gameOverUI.GetComponent<GameOverUI>().updateGameOverUI(reachedScoreInSession, userData.scoreRecord);
        
        saveProgress(reachedScoreInSession);
    }

    private void saveProgress(int reachedScore)
    {
        if (userData.scoreRecord < reachedScore)
        {
            Debug.Log("Saving new user record!");
            userData.scoreRecord = reachedScore;
        }

        userData.coinBalance += collectedCoinInSession;
        collectedCoinInSession = 0;

        loadAndSaveSystem.saveUserData(userData);
    }

    public void restartGame()
    {
        sceneNavigator.loadPlayScene();
    }

    public float getDifficultyFactor(float initTimeOffset)
    {
        return Mathf.Clamp((100 - Mathf.Sqrt(1 + (Time.timeSinceLevelLoad - initTimeOffset) * 5.25f)) / 100, 0.33f, 1.0f);
    }

    public bool isMaxLife()
    {
        return lifes == maxLifes;
    }

    public void collectCoin(int value)
    {
        collectedCoinInSession += value;
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
