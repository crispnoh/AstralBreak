using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    //script for buttons, levers, and the like
    public bool active;
    public bool toggleable;
    public float displacement;
    public GameObject obstacle;

    bool able = false;

    GameObject player;
    PlayerControl playerScript;
    

    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        playerScript.input.Gameplay.Interact.performed += ctx =>
        {
            if (able) { Activate(); }
        };
    }

    void OnTriggerEnter() //while player is in range, so player doesnt hit a lever from across the map
    {
        able = true;
    }
    void OnTriggerExit()
    {
        able = false;
    }

    void Activate()
    {
        // if inactive, active. if it is active and toggleable, toggle, if not, dont toggle. 
        //Debug.Log("activated");

        if (!active) 
        {
            active = true;
            //wallCollider.enabled = !wallCollider.enabled;
            MoveObject();
        }
        else if(active && toggleable) 
        { 
            active = false;
            //wallCollider.enabled = !wallCollider.enabled;
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (!active)
        {
            float y = obstacle.transform.position.y + displacement;
            while (obstacle.transform.position.y < y) //moves object back up if button/lever was flipped off
            {
                obstacle.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            }
        }
        else
        {
            float y = obstacle.transform.position.y - displacement;
            while (obstacle.transform.position.y > y) //moves object down if button/lever was flipped on
            {
                obstacle.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            }
        }
    }
}
