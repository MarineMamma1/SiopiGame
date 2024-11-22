using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public RespawnManager manager;
        
        
        public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

        public void Restart()
    {
        PlayerManager.Instance.transform.position = manager.activeCollider.charPos;
        Time.timeScale = 1;
        GameManager.health = 4;
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
