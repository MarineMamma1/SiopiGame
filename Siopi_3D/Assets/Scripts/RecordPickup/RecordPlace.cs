using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlace : MonoBehaviour
{
    public RecordGate gate;
    public bool placed;
    private bool selectable;
    public GameObject record;
    public GameManager.RecordType requiredRecord;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (selectable && !placed && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.HasRecord(requiredRecord))
            {
                placed = true;
                GameManager.Instance.UseRecord(requiredRecord);
                record.SetActive(true);
                gate.CheckHowMany();
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(GameManager.Instance.HasRecord(requiredRecord))
            {
                selectable = true;
                Debug.Log($"Nice {requiredRecord}, loser. Yoink.");
            }
            else
            {
                selectable = false;
                Debug.Log($"Sorry Siopi, I can't give credit. Come back when you've, mmmm, got the {requiredRecord}");
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
                selectable = false;
                Debug.Log("Player Has Left Selectable Area");
        }
    }
}
