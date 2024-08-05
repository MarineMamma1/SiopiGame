using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedDebugUI : MonoBehaviour
{
    public CharacterController3D playerController;
    public TextMeshProUGUI speedText;

    private void Update()
    {
        if (playerController != null && speedText != null)
        {
            speedText.text = $"Speed: {playerController.CurrentSpeed:F2} m/s";
        }
    }
}