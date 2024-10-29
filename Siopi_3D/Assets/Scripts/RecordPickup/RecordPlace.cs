using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlace : MonoBehaviour
{
    public RecordGate gate;
    public bool placed;
    private bool selectable;
    public GameObject record;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable && placed == false)
        {
            if(Input.GetKeyDown(KeyCode.E))
        {
            placed = true;
            PlayerManager.Instance.RecordAmount--;
            record.SetActive(true);
            gate.CheckHowMany();
            
        }
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(PlayerManager.Instance.RecordAmount >= 1)
            {
                selectable = true;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(PlayerManager.Instance.RecordAmount >= 1)
            {
                selectable = false;
            }
        }
    }
}
