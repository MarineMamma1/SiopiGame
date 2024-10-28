using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObj : MonoBehaviour
{
    private bool selectable;
    public bool returner;
    public Vector3 returnLocation;
    void LateUpdate()
    {
        if(selectable)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.Instance.RecordAmount++;
                transform.GetChild(0).gameObject.SetActive(false);
                if (returner == true)
                {
                    PlayerManager.Instance.transform.position = returnLocation;
                }
            }
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            selectable = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            selectable = false;
        }
    }
}
