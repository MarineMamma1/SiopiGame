using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator anim;  
    //private bool Isopen = false;
    private PlayerManager keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = FindObjectOfType<PlayerManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{
       // if(Isopen == true && keys.KeyAmount >= 1)
            //{
              //  keys.KeyAmount -= 1;   
              //  anim.SetTrigger("OpenDoor");
              //  Isopen = false; 
           // }
    //}

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player" && keys.KeyAmount >= 1)
            {
                keys.KeyAmount -= 1;
                anim.SetTrigger("OpenDoor");
            }
            
        }

        //private void OnTriggerExit(Collider other) 
        //{
        //    anim.SetTrigger("CloseDoor");
        //}

}
