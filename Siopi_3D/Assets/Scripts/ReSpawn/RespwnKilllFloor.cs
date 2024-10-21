using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwnKilllFloor : MonoBehaviour
{
    public GameObject player;
    public RespawnManager manager;
    void Start()
    {
        player = FindObjectOfType<CRE311PlayerMovement>().gameObject;
        manager = transform.parent.GetComponent<RespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {Debug.Log("I am working");
            player.transform.position = manager.activeCollider.charPos;
        }
    }
}
