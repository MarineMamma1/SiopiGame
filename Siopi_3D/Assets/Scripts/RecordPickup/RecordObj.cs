using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObj : MonoBehaviour
{
    private bool selectable;
    public bool returner;
    public Vector3 returnLocation;

    // I learned that this allows it to realise which record is which! 
    public GameManager.RecordType recordType;

    void LateUpdate()
    {
        if (selectable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // I think this is how Instance works
                GameManager.Instance.AddRecord(recordType);
                transform.GetChild(0).gameObject.SetActive(false);
                if (returner)
                {
                    PlayerManager.Instance.transform.position = returnLocation;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selectable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selectable = false;
        }
    }
}