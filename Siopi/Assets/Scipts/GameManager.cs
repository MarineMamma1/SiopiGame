using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        MainMenu,
        InGame,
        Pause,
        GameOver,
        Win
    }

    public GameState CurrentState { get; private set; }
    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject gameUI; // HUD when in game
    public GameObject winScreenUI; // Add reference to the win screen UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        OnStateChange(newState);
    }

    private void OnStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                ShowOnly(mainMenuUI);
                LoadScene("MainMenu");
                break;
            case GameState.InGame:
                ShowOnly(gameUI);
                LoadScene("GameScene");
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                ShowOnly(pauseMenuUI);
                break;
            case GameState.GameOver:
                ShowOnly(gameOverUI);
                break;
            case GameState.Win:
                ShowOnly(winScreenUI);
                break;
        }
    }

    private void ShowOnly(GameObject activeUI)
    {
        mainMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(false);
        winScreenUI.SetActive(false);

        activeUI.SetActive(true);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
       // if (CurrentState == GameState.InGame && Input.GetKeyDown(KeyCode.Escape))
      //  {
      //      ChangeState(GameState.Pause);
       // }
    }

    public void OnMenu()
    {
        Debug.Log("pressed");
        if (CurrentState == GameState.InGame)
        {
            ChangeState(GameState.Pause);
        }
    }

    public void ResumeGame()
    {
        ChangeState(GameState.InGame);
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        ChangeState(GameState.GameOver);
    }

    public void StartGame()
    {
        ChangeState(GameState.InGame);
    }

    public void WinGame()
    {
        ChangeState(GameState.Win);
    }

    public void Quit()
    {
        Application.Quit();
    }

    
}
