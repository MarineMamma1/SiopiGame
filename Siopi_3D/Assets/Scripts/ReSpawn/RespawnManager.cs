using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<RespawnCollider> colliders;
    public RespawnCollider activeCollider;
    void Start()
    {
        colliders.AddRange(FindObjectsOfType<RespawnCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
