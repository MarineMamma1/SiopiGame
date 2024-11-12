using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject WinPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 1;
            WinPanel.SetActive (true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
