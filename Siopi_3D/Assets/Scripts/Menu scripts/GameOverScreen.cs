using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public GameObject pauseMenu;
    public void MenuButtonSet()
    {
        SceneManager.LoadScene("Main Menu");
    }

        public void Resetlevel()
    {
        SceneManager.LoadScene("Siopi_Main");
    }
}