using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour
{
    public void MenuButtonSet()
    {
        SceneManager.LoadScene("Main Menu");
    }
}