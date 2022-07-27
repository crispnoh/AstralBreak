using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwap : MonoBehaviour
{
    PlayerControl controls;
    RespawnScript tp;
    CameraControl camScript;
    GameObject cam;
    GameObject player;

    public GameObject tpSpot;
    float x;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
        camScript = cam.GetComponent<CameraControl>();
        controls = player.GetComponent<PlayerControl>();
        tp = player.GetComponent<RespawnScript>();
    }

    void OnTriggerEnter(Collider entity)
    {
        if (entity.tag == "Player")
        {
            //Debug.Log("Teleport player");
            SwapWorld();
        }
        
    }

    public void SwapWorld() //changes controls, camera angle, tps player
    {
        //controls
        controls.ToggleControls();

        //teleport
        x = tpSpot.transform.position.x;
        z = tpSpot.transform.position.z;
        tp.Teleport(x,z);
        
        // perspective changes
        if (!controls.controlDisabled)
        {
            player.transform.eulerAngles = new Vector3(45, 0, 0);
            cam.transform.eulerAngles = new Vector3(40, 0, 0);
            camScript.horizontalOffset = -10;
            camScript.verticalOffset = -1;
        }
        else
        {
            player.transform.eulerAngles = new Vector3(15, 0, 0);
            cam.transform.eulerAngles = new Vector3(33, 0, 0);
            camScript.verticalOffset = 3;
            camScript.horizontalOffset = -8;
        }
    }

}
