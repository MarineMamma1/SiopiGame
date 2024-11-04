using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Record currentRecord;
    
    private void Update()  
    {
        if (currentRecord != null && Input.GetKeyDown(KeyCode.E))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddRecord(currentRecord.recordType);
                Destroy(currentRecord.gameObject);
                currentRecord = null;
            }
        }
    }

    public void SetCurrentRecord(Record record)
    {
        currentRecord = record;
    }

    internal void SetCurrentRecord(RecordType recordType)
    {
        throw new NotImplementedException();
    }
}
