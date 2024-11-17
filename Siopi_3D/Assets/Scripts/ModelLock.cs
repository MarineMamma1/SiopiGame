using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.localPosition != new Vector3(0, -0.951f, 0))
        {
            transform.localPosition = new Vector3(0, -0.951f, 0);
        }
    }
}
