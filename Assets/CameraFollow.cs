using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    List<Transform> targets = new List<Transform>();

    void Start()
    {
        foreach (var player in Object.FindObjectsOfType<Player>())
        {
            targets.Add(player.transform);
        }

        if (targets.Count > 0)
            transform.position = GetTargetPos();
    }


    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        //Vector3 targetCamPos = target.position + offset;
        if(targets.Count > 0)
        {
            Vector3 targetPos = GetTargetPos();
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
        }
    }

    Vector3 GetTargetPos()
    {
        Vector3 targetCamPos;
        if (targets.Count == 2)
            targetCamPos = (targets[0].transform.position + targets[1].transform.position) / 2;
        else
            targetCamPos = targets[0].transform.position;

        return new Vector3(targetCamPos.x, targetCamPos.y, transform.position.z);

        


        
    }
}

