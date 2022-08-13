using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    //its called respawn but tbh im gonna use it for teleports too, just set respawn point to the new area and then tp there
    //bc its probably gonna be a "save point" anyways
    public GameObject respawnPoint;

    void Start()
    {
        respawnPoint = GameObject.Find("RespawnPoint");
    }

    //sends player to respawn point, probably will have code here that respawns enemies, resets world aswell.
    public void Respawn()
    {
        transform.position = respawnPoint.transform.position;
    }

    //basically respawn but moves respawn before moving player.
    public void Teleport(float x, float z)
    {
        respawnPoint.transform.position = new Vector3(x, 1, z);
        transform.position = respawnPoint.transform.position;
    }
}
