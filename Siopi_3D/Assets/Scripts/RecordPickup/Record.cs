using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    public enum RecordType
    {
        A,
        B,
        C,
    }

    public RecordType recordType;

    public void Interact()
    {
        GameManager.Instance.PickupRecord(recordType);
        Destroy(gameObject);
    }
}
