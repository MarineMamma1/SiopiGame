using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionScript : MonoBehaviour
{

public GameObject DetectionCollider;
public Transform player;
public Transform target;
public Transform targetObj;

    void OnTriggerStay(Collider target)
{
    if(target.tag == "Player")
    {
        Debug.Log("Player Has Been Detected");
        transform.LookAt(target.gameObject.transform);
        gameObject.GetComponent<NavMeshAgent>().SetDestination(player.position);  
    }
}
}
