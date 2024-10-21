using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCollider : MonoBehaviour
{
    public bool checking;
    public Vector3 charPos;
    public CRE311PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<CRE311PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            checking = true;
            transform.parent.GetComponent<RespawnManager>().activeCollider = this;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(checking == true && player.grounded == true)
        {
            charPos = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            checking = false;
        }
    }
}
