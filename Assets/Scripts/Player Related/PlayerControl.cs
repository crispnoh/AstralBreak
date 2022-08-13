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
    public Animator anim;
    PlayerStats stats;
    UIFunctions ui;
    RespawnScript respawn;
    MouseControl mouse;

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

    [Space(10)]

    [Header("Ability Stats")]
    public GameObject grenadePrefab;
    public float throwForce = 6f;

    // Private Variables
    private float baseMoveSpeed;
    private Rigidbody rb;
    private Vector2 mousev2;
    private Vector2 mousev2Norm;

    // bool for using ability/dash 
    private bool dashAble = true;
    private bool abilityAble = false;
    private bool shootAble = false;

    private Vector2 movementInput; //movement input value

    //mainly detecting inputs and acting on them
    void Awake()
    {
        input = new MainControls();

        stats = GetComponent<PlayerStats>();
        ui = GetComponent<UIFunctions>();
        rb = GetComponent<Rigidbody>();
        respawn = GetComponent<RespawnScript>();
        mouse = GetComponent<MouseControl>();
        baseMoveSpeed = moveSpeed;

        //Cursor.visible = lockCursor;
        //Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;

        input.Gameplay.Aim.performed += ctx =>
        {
            mousev2 = ctx.ReadValue<Vector2>();
            //mousev2Norm = mousev2.normalized;
            //Debug.Log(mousev2Norm);
            //anim.SetFloat("aimY", mousev2Norm.x);
            //anim.SetFloat("aimY", mousev2Norm.y);
            mouse.MouseInput(mousev2); //mouse input
        };

        input.Gameplay.Movement.performed += ctx => //if movement input is detected
        {
            //Debug.Log(ctx.ReadValueAsObject());
            anim.SetBool("isWalking", true);
            movementInput = ctx.ReadValue<Vector2>();
            anim.SetFloat("dirX", movementInput.x);
            anim.SetFloat("dirY", movementInput.y);
        };
        input.Gameplay.Movement.canceled += ctx => //to stop moving
        {
            //Debug.Log(ctx.ReadValueAsObject());
            movementInput = Vector2.zero;
            anim.SetBool("isWalking", false);
        };

        input.Gameplay.Shoot.performed += ctx => //if shooting input is detected
        {
            OnShoot(ctx);
        };

        input.Gameplay.Ability.performed += ctx => //if ability input is detected
        {
            OnAbility(ctx); 
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

    void Start()
    {
        Time.timeScale = 1f;
        if (controlDisabled) { DisableControls(); }
        else { EnableControls(); }
    }

    public void ToggleControls()
    {
        controlDisabled = !controlDisabled;
        if (controlDisabled) { DisableControls(); }
        else { EnableControls(); }
    }

    public void EnableControls()
    {
        controlDisabled = false;
        dashAble = true;
        if (astralWorld)
        {
            shootAble = true;
            abilityAble = true;
        }
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

    void Fire() // function for actively shooting, spawns a projectile and starts coroutine to simulate fire rate
    {
        if (shootAble)
        {
            anim.SetBool("isAttacking", true);
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(firePoint.up * fireForce, ForceMode.Impulse);
            StartCoroutine(shooting());
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        // function for shooting input
        if (context.started) //shoot button pressed
        {
            //Debug.Log("shooting");
            InvokeRepeating("Fire", 0, fireRate - 0.1f); //repeats the fire function
        }
        if (context.canceled) //shoot button released
        {
            //Debug.Log("stopped shooting");
            anim.SetBool("isAttacking", false);
            CancelInvoke("Fire");
        }
    }

    public void OnAbility(InputAction.CallbackContext context)
    {   // function for using ability
        if (abilityAble)
        {
            //Debug.Log("ability used");
            GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            grenade.GetComponent<Rigidbody>().AddForce(firePoint.up * throwForce, ForceMode.Impulse);
            
            abilityAble = false;
            StartCoroutine(ability());
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {   // function for using dash
        if (dashAble) 
        {
            //Debug.Log("dashed");
            dashAble = false;
            StartCoroutine(dash());
        }
    }

    // coroutines for dash and ability cooldowns, as well as fire rate
    IEnumerator shooting()
    {
        shootAble = false;
        yield return new WaitForSeconds(fireRate);
        shootAble = true;
    }

    IEnumerator ability()
    {
        // do ability function here 
        yield return new WaitForSeconds(abilityCDTime);
        abilityAble = true;
    }
    
    IEnumerator dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
	        anim.SetBool("isDashing", true);
            moveSpeed = dashSpeed;
            rb.useGravity = false;

            yield return null;
        }
	    anim.SetBool("isDashing", false);
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
