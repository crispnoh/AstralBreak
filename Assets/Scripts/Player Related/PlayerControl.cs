using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //theres a bug where if the player goes from in game to the main menu and back
    //they lose control over their movement until they pause and unpause the game
    //and idk how to fix it 

    public MainControls input;
    PlayerStats stats;
    UIFunctions ui;
    RespawnScript respawn;

    [Header("Ignore")]
    public bool controlDisabled = true; //probably coulda named it better but this is for switching between the astral and over world
    public bool astralWorld = false;
    //public bool lockCursor = true;

    [Space(10)]

    [Header("Move/Dash Stats")]
    public float moveSpeed = 1f;
    public float dashSpeed = 1.5f;
    public float dashTime = 1.5f;

    [Header("Move/Dash Cooldowns & Cost")]
    public int dashCDTime = 2;
    public int abilityCDTime = 5;
    public int abilityCost = 10;

    [Space(10)]

    [Header("Firing Stats")]
    public GameObject projectilePrefab;
    public Transform firePoint; //where projectiles are born
    public float fireRate = 1f; //interval between shots.
    public float fireForce = 6f; //strength of projectile launch

    // Private Variables
    private float baseMoveSpeed;
    private Rigidbody rb;

    // bool for using ability/dash 
    private bool dashAble = true;
    private bool abilityAble = false;
    private bool shootAble = false;

    private Vector2 movementInput; //movement input value

    //mainly detecting inputs and acting on them
    void Awake()
    {
        baseMoveSpeed = moveSpeed;
        input = new MainControls();
        stats = GetComponent<PlayerStats>();
        ui = GetComponent<UIFunctions>();
        rb = GetComponent<Rigidbody>();
        respawn = GetComponent<RespawnScript>();
        if (controlDisabled) { DisableControls(); }
        else { EnableControls(); }
        //Cursor.visible = lockCursor;
        //Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;

        input.Gameplay.Movement.performed += ctx => //if movement input is detected
        {
            //Debug.Log(ctx.ReadValueAsObject());
            movementInput = ctx.ReadValue<Vector2>();
        };
        input.Gameplay.Movement.canceled += ctx => //to stop moving
        {
            //Debug.Log(ctx.ReadValueAsObject());
            movementInput = Vector2.zero;
        };

        input.Gameplay.Shoot.performed += ctx => //if shooting input is detected
        {
            OnShoot(ctx);
        };

        input.Gameplay.Ability.performed += ctx => //if ability input is detected
        {
            if (stats.currentMana >= abilityCost ) { OnAbility(ctx); }
        };
        
        input.Gameplay.Dash.performed += ctx => //if dash input is detected
        {
            OnDash(ctx);
        };

        input.Gameplay.Pause.performed += ctx => //if pause input is detected 
        {
            //open pause menu, lock/unlock mouse, probably swap action map here too
            //lockCursor = !lockCursor;
            //Cursor.visible = lockCursor;
           // Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;

            ui.pauseGame();
            ToggleControls();
        };
    }

    public void ToggleControls()
    {
        controlDisabled = !controlDisabled;

        if (controlDisabled)
        {
            if (ui.gamePaused) { dashAble = false; }
            shootAble = false;
            abilityAble = false;
        }
        else
        {
            dashAble = true;
            shootAble = true;
            abilityAble = true;
        }
    }

    public void EnableControls()
    {
        controlDisabled = false;
        dashAble = true;
        shootAble = true;
        abilityAble = true;
    }

    public void DisableControls()
    {
        controlDisabled = true;
        if (ui.gamePaused) { dashAble = false; }
        shootAble = false;
        abilityAble = false;
    }

    void FixedUpdate()
    {
        // gets the player to move using vectors, if player falls to a certain height, gets respawned
        Vector3 m = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(m, Space.World);
        if (transform.position.y <= -5) { respawn.Respawn(); }
    }

    void Fire() // function for actively shooting
    {
        if (shootAble)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(firePoint.up * fireForce, ForceMode.Impulse);
            StartCoroutine(shooting());
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        // function for shooting
        if (context.started) //shoot button pressed
        {
            //Debug.Log("shooting");
            InvokeRepeating("Fire", 0, fireRate - 0.1f); //repeats the fire function
        }
        if (context.canceled) //shoot button released
        {
            //Debug.Log("stopped shooting");
            CancelInvoke("Fire");
        }
    }

    public void OnAbility(InputAction.CallbackContext context)
    {   // function for using ability
        if (abilityAble)
        {
            //Debug.Log("ability");
            abilityAble = false;
            StartCoroutine(ability());
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {   // function for using dash
        if (dashAble) 
        { 
            //Debug.Log("dash is on cooldown");
            dashAble = false;
            StartCoroutine(dash());
        }
    }

    IEnumerator shooting()
    {
        shootAble = false;
        yield return new WaitForSeconds(fireRate);
        shootAble = true;
    }

    IEnumerator ability()
    {
        // do ability function here 
        stats.UseMana(abilityCost);
        yield return new WaitForSeconds(abilityCDTime);
        abilityAble = true;
    }

    // coroutines for dash and ability cooldowns
    IEnumerator dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            moveSpeed = dashSpeed;
            rb.useGravity = false;

            yield return null;
        }
        moveSpeed = baseMoveSpeed;
        rb.useGravity = true;

        yield return new WaitForSeconds(dashCDTime);
        dashAble = true;
        //Debug.Log("dash is up");
    }

    void OnEnable()
    {
        input.Gameplay.Enable();
    }

    void OnDisable()
    {
        input.Gameplay.Disable();
    }
}
