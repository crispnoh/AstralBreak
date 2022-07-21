using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControl : MonoBehaviour
{
    //mouse control + rotating the player 
    // main camera and a mask that makes the raycast only hit layer specified
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layerMask;

    // target is where the player will be looking
    public float rotateTime;
    public GameObject target;
    Vector2 mousePos;

    void Awake()
    {
        GetComponent<PlayerControl>().input.Gameplay.Aim.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // reads player's mouse input, raycasts it to the world, and rotates player accordingly.
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask)) 
        { 
            target.transform.position = raycastHit.point;

            //where we want the player character to be facing, removing the x and z rotation.
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateTime * Time.deltaTime);
        }
    }
}
