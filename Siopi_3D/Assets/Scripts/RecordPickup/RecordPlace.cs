using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlace : MonoBehaviour
{
    public RecordGate gate;
    public bool placed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(PlayerManager.Instance.RecordAmount >= 1)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    placed = true;
                    PlayerManager.Instance.RecordAmount--;
                    gate.CheckHowMany();
                }
            }
        }
    }
}
