using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class violinProjBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trugger");
        if (collision.gameObject.CompareTag("Enemy"))
           {
        Destroy(this.gameObject);

          } else if  (collision.gameObject.CompareTag("Ground"))
        {
            
            Destroy(this.gameObject);
        }
    }
}
