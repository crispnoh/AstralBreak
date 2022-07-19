using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public GameObject projectilePrefab;
    //public Transform firePoint;
    //public float fireForce = 20f; //heh

    public void Fire()
    {
        // sends a projectile prefab flying
        //GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        //projectile.GetComponent<Rigidbody>().AddForce(firePoint.up * fireForce, ForceMode.Impulse);
    }
}
