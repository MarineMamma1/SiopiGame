using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordGate : MonoBehaviour
{
    public List<RecordPlace> statues;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckHowMany()
    {
        int amountDone = 0;
        for(int i = 0; i < statues.Count; i++)
        {
            if(statues[i].placed == true)
            {
                amountDone++;
            }
        }
        if(amountDone == statues.Count)
        {
            
        }
    }
}
