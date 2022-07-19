using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControl : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    Vector2 mousePos;
    
    void Awake()
    {
        GetComponent<PlayerControl>().input.Gameplay.Aim.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // reads players mouse input and rotates player accordingly.
        //Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
