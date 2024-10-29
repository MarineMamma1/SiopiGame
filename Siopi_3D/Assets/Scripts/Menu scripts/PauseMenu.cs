using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

        public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Siopi_Main");
    }
        public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf == false)
            {
                pauseMenu.SetActive (true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            else
            {
               pauseMenu.SetActive (false);
               Time.timeScale = 1;
            }
        }
    }
    
}
