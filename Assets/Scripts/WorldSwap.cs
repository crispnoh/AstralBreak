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
        
        //camera // prolly gonna get scrapped
        /*if (!controls.controlDisabled)
        {
            player.transform.eulerAngles = new Vector3(0, 0, 0);
            cam.transform.eulerAngles = new Vector3(45, 0, 0);
            camScript.horizontalOffset = -5;
            camScript.verticalOffset = 5;
        }
        else
        {
            player.transform.eulerAngles = new Vector3(90, 0, 0);
            cam.transform.eulerAngles = new Vector3(90, 0, 0);
            camScript.horizontalOffset = -10;
            camScript.verticalOffset = 0;
        }*/
    }

}
