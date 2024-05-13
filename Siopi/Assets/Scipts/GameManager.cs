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
        GameOver
    }

    public GameState CurrentState { get; private set; }
    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject gameUI; // HUD when in game
    // add more


    private void Awake()
    {
        //Get player state controller;;;
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
      //  ChangeState(GameState.MainMenu);
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
        }
    }

    private void ShowOnly(GameObject activeUI)
    {
         mainMenuUI.SetActive(false);
         gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(false);

        activeUI.SetActive(true);
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
       
        
        //if gamestate ingame and press esc -> set state pause
    }

    public void OnMenu()
    {
        Debug.Log("pressed");
        if(CurrentState == GameState.InGame)
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

    public void Quit()
    {
        Application.Quit();
    }

   
  
}
