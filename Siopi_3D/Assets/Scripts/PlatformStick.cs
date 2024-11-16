using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    private Transform originalParent;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(originalParent);
        }
    }
}