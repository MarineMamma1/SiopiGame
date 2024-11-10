using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordObj : MonoBehaviour
{
    private bool selectable;
    public bool returner;
    public Vector3 returnLocation;

    // Add a field to specify which record type this is
    public GameManager.RecordType recordType;

    void LateUpdate()
    {
        if (selectable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
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