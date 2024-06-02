using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public bool isOpen = false;

    public void ActivateDoor()
    {
        isOpen = true;
        Debug.Log("Door activated!");
        Destroy(this.gameObject);
        //
        // Add additional logic here for door animation or scene transition
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TwoDCharacterController twoDCharacterController = collision.GetComponent<TwoDCharacterController>();
            Debug.Log("Playaaaa");   
               if (twoDCharacterController.isBDoorOpen)
            {
                // ActivateDoor();
                GameManager.Instance.WinGame();

            }
        }
    }
}
