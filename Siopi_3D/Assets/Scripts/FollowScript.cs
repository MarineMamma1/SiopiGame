using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;
    public Transform targetObj;

    private void Update
    {
        //LookForPlayer();
    }

    private PlayerManager LookForPlayer()
    {
        if (PlayerManager.Instance == null)
        {
            return null;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = PlayerManager.Instance.transform.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            Debug.Log("Player Has Been Detected");

            return PlayerManager.Instance;
        }
        
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, targetObj.position, 1 * Time.deltaTime);
    }
}
