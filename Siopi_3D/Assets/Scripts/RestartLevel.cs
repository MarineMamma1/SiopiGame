using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    public void RestartGame() 
      {
        Time.timeScale = 1;
        GameManager.health = 4;
    		SceneManager.LoadScene(0);
    	}
    
    


 //private void Awake() 
   // {
     //   Time.timeScale = 1;
     //   GameManager.health = 4;
    //    SceneManager.LoadScene("Greybox");
   // }

}