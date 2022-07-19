using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use this code for 2D elements in game
public class Billboard : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam.transform);
    }
}
