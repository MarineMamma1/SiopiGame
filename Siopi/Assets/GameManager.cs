using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ChangeState(GameState.MainMenu);
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
                LoadScene("MainMenu");
                break;
            case GameState.InGame:
                LoadScene("GameScene");
                break;
            case GameState.Pause:
                // Implement pause logic here
                break;
            case GameState.GameOver:
                LoadScene("GameOver");
                break;
        }
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
}
