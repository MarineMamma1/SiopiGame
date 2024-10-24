using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObj : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.Instance.RecordAmount++;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
