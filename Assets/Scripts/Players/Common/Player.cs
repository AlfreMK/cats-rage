using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update



    public Player teammate;
    public Animator animator;
    public static Player Instance;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int health = 100;
    [SerializeField] public float verticalSpeed = 2f;
    [SerializeField] public float horizontalSpeed = 4f;
    [SerializeField] public int playerNumber = 1;

    private int movingDirection = 1;
    private string horizontalKey;
    private string verticalKey;
    private string shootKey;
    private string punchKey;


    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttacking = Animator.StringToHash("Shoot");
    private static readonly int _animationRunning = Animator.StringToHash("Walk");
    private static readonly int _animationSpecial = Animator.StringToHash("Special");

    Vector2 vector_position;
    Vector2 control;
    private Rigidbody2D rb;
    private SpriteRenderer sr;




    void Awake()
    {
        //GameObject.DontDestroyOnLoad(this.gameObject);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Set control keys based on player number
        if (playerNumber == 1)
        {
            // To set the keys, go to Edit > Project Settings > Input Manager
            horizontalKey = "Horizontal";
            verticalKey = "Vertical";
            shootKey = "Shoot";
            punchKey = "Punch";
        }
        else if (playerNumber == 2)
        {
            horizontalKey = "Horizontal2";
            verticalKey = "Vertical2";
            shootKey = "Shoot2";
            punchKey = "Punch2";
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        control = new Vector2(Input.GetAxisRaw(horizontalKey), Input.GetAxisRaw(verticalKey));
        if (control.x != 0 && GameManager.Instance.IsInputEnabled() && !PauseMenu.GameIsPaused)
        {
            transform.rotation = Quaternion.Euler(0, control.x > 0 ? 0 : 180, 0);
            movingDirection = control.x > 0 ? 1 : -1;
        }
        if (!PauseMenu.GameIsPaused)
        {
            animationController();
        }
        if (GameManager.Instance.IsInputEnabled()){
            handleMovement(control);    
        }
    }


    void animationController()
    {
        if (GameManager.Instance.IsInputEnabled())
        {
            if (Input.GetAxisRaw(shootKey) != 0)
            {
                SetAnimationState(_animationAttacking);
            }
            else if (Input.GetAxisRaw(punchKey) != 0)
            {
                SetAnimationState(_animationSpecial);
            }
            else if (control.magnitude != 0)
            {
                SetAnimationState(_animationRunning);
            }
            else
            {
                SetAnimationState(_animationIdle);
            }
        }
        else
        {
            SetAnimationState(_animationIdle);
        }
    }

    public void teleport()
    {
        vector_position = new Vector2(-10, 0);
        transform.position = vector_position;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Debug.Log("Player " + playerNumber + " has " + health + " health");
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }


    void handleMovement(Vector2 control)
    {
        rb.velocity = new Vector2(control.x * horizontalSpeed, control.y * verticalSpeed);    
    }

}
