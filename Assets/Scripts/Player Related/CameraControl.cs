using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //script to make camera follow player with damping for smooth movement
    //be careful when changing the camera offsets. they are relative to the player's rotation so if the player is rotated 90, vertical will move horizontally, and vice versa for horizontal. 
    public Transform player;
    public float smoothTime = 0.3f;
    public int verticalOffset = 5;
    public int horizontalOffset = -5;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 targetPosition = player.TransformPoint(new Vector3(0, verticalOffset, horizontalOffset));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
