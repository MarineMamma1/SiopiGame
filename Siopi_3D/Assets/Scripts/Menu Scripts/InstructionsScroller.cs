using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsScroller : MonoBehaviour
{
    public List <GameObject> InstructionObjects;
    public int currentInstruction;
    public GameObject PausePanel;

    public void BackButton()
    {
        PausePanel.SetActive (true);
        gameObject.SetActive (false);
    }

    public void scroll()
    {
        currentInstruction ++;
        if (currentInstruction < InstructionObjects.Count)
        {
            InstructionObjects[currentInstruction -1] .SetActive (false);
            InstructionObjects[currentInstruction] .SetActive (true);
        }

        else if (currentInstruction == InstructionObjects.Count)
        {
            currentInstruction = 0;
            InstructionObjects [InstructionObjects.Count -1] .SetActive (false);
            InstructionObjects[currentInstruction] .SetActive (true);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive (false);            
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            gameObject.SetActive(false);
        }
    }
    public void scrollBack()
    {
        currentInstruction --;
        if (currentInstruction >= 0)
        {
            InstructionObjects[currentInstruction +1] .SetActive (false);
            InstructionObjects[currentInstruction] .SetActive (true);
        }

        else if (currentInstruction < 0)
        {
            currentInstruction = InstructionObjects.Count-1;
            InstructionObjects [0] .SetActive (false);
            InstructionObjects[currentInstruction] .SetActive (true);
        }
    }

}
