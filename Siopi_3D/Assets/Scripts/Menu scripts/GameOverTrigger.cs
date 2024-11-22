using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject WinPanel;

    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            WinPanel.SetActive (true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
