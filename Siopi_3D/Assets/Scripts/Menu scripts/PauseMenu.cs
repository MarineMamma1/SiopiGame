using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Image InstructionsImage;

    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }


    public void InstructionsButton()
    {
        InstructionsImage.SetActive (true);
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