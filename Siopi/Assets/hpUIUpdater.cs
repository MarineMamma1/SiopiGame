using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hpUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public string textTemplate = "HP: {0}";
    public float healthAmount = 0f;
    private PlayerStateController playerStateController;
    //public GameObject NewPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerStateController = FindObjectOfType<PlayerStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateController != null)
        {
            if (textMeshProUGUI != null)
            {
                healthAmount = playerStateController.CurrentHealth;
                textMeshProUGUI.text = string.Format(textTemplate, healthAmount);
            }
            else
            {
                Debug.Log("woops");
            }
        } else
        {
            playerStateController = FindObjectOfType<PlayerStateController>();
            Debug.Log(playerStateController);
        }
    }
}
