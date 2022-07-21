using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float time = 3f;
    private void OnCollisionEnter(Collision collision)
    {
        //damage if enemy hit
        Destroy(gameObject);
    }
    
    private void Start()
    {
        Destroy(gameObject, time);
    }
}
