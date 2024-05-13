using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugUIHum : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public string textTemplate = "Pitch: {0}";
    public float pitch = 0.0f;
    private AudioPitchControllerInputSystem pitchController;
  //  public GameObject HummingComponent;
  //variable
    // Start is called before the first frame update
    void Start()
    {
         pitchController = FindObjectOfType<AudioPitchControllerInputSystem>();
        
        Debug.Log(pitchController);
    }

    // Update is called once per frame
    void Update()
    {
        if (pitchController != null)
        {
            if (textMeshProUGUI != null)
            {
                pitch = pitchController.pitchValue;
                textMeshProUGUI.text = string.Format(textTemplate, pitch);
            }
            else
            {
                Debug.Log("failed");
            }
        }
        else
        {
            pitchController = FindObjectOfType<AudioPitchControllerInputSystem>();
            Debug.Log(pitchController);

        }

    }
}
