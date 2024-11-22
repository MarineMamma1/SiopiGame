using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject InstructionsPanel;
    public RespawnManager manager;

    public void MenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void InstructionButton()
    {
        InstructionsPanel.SetActive (true);
        gameObject.SetActive (false);
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
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
               InstructionsPanel.SetActive (false);
            }
        }
    }
    public void Restart()
    {
        PlayerManager.Instance.transform.position = manager.activeCollider.charPos;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}