using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseWeapon : MonoBehaviour
{
    // Combo timing and states
    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboWindow = 1f; // Time allowed between combo steps

    // Rotation and position variables
    public Transform weaponHolderTransform; // Reference to the WeaponHolder
    public Transform weaponTransform; // Reference to the Weapon
    private bool isAttacking = false;
    private Quaternion originalWeaponRotation;
    private Quaternion targetWeaponRotation;
    private Vector3 originalHolderPosition;
    private Vector3 targetHolderPosition;
    private float swingSpeed = 1f; // Speed of the swing

    private void Start()
    {
        // Set initial weapon holder position
        weaponHolderTransform.localPosition = new Vector3(0f, 1f, 0.5f);
    }

    // Called when the attack input is received
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isAttacking)
        {
            PerformAttack();
        }
    }

    // Perform the attack and handle combo logic
    private void PerformAttack()
    {
        if (Time.time - lastAttackTime > comboWindow)
        {
            comboStep = 0; // Reset the combo if the window has passed
        }

        comboStep++;
        comboStep = Mathf.Clamp(comboStep, 1, 3); // Ensure comboStep is between 1 and 3

        // Trigger the attack animation
        StartCoroutine(AttackRoutine());

        Debug.Log("Combo Step: " + comboStep);

        lastAttackTime = Time.time;

        // Reset combo if it's the final attack
        if (comboStep >= 3)
        {
            comboStep = 0;
            Debug.Log("Combo Completed");
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        // Save the original states
        originalWeaponRotation = weaponTransform.localRotation;
        originalHolderPosition = weaponHolderTransform.localPosition;

        // Set targets based on the combo step
        Vector3 startPosition = originalHolderPosition;
        Vector3 endPosition = originalHolderPosition;
        Quaternion startRotation = originalWeaponRotation;
        Quaternion endRotation = originalWeaponRotation;

        if (comboStep == 1)
        {
            // First attack: Baseball bat swing (right to left)
            endRotation = originalWeaponRotation * Quaternion.Euler(0f, 0f, -90f); // Simulate a full right-to-left swing
            endPosition = originalHolderPosition + new Vector3(-0.5f, 0f, 0f); // Move slightly left
        }
        else if (comboStep == 2)
        {
            // Second attack: Backhand tennis racket hit (right-handed)
            endRotation = originalWeaponRotation * Quaternion.Euler(0f, 0f, 45f); // Simulate a diagonal backhand hit
            endPosition = originalHolderPosition + new Vector3(0.5f, 0f, 0f); // Move slightly right
        }
        else if (comboStep == 3)
        {
            // Final attack: Overhead axe swing
            endRotation = originalWeaponRotation * Quaternion.Euler(90f, 0f, 0f); // Simulate an overhead chop
            endPosition = originalHolderPosition + new Vector3(0f, -1f, -0.5f); // Move down and back
        }

        // Animate the weapon holder and weapon
        float elapsedTime = 0f;
        float duration = 0.5f; // Time to complete the swing

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            weaponTransform.localRotation = Quaternion.Lerp(startRotation, endRotation, t);
            weaponHolderTransform.localPosition = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }

        // Ensure final position is reached
        weaponTransform.localRotation = endRotation;
        weaponHolderTransform.localPosition = endPosition;

        // Return to original position
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            weaponTransform.localRotation = Quaternion.Lerp(endRotation, startRotation, t);
            weaponHolderTransform.localPosition = Vector3.Lerp(endPosition, startPosition, t);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }

        // Ensure weapon holder returns to the original position
        weaponTransform.localRotation = startRotation;
        weaponHolderTransform.localPosition = startPosition;

        isAttacking = false;
    }
}
