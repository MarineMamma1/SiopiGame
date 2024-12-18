using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
        GameManager gameManager;
        [SerializeField] private Transform player;
        [SerializeField] private Transform respawn_point;
        

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.health -= 1;
            player.transform.position = respawn_point.transform.position;
        }
    }
}
