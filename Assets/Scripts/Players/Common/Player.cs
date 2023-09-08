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
    // -4.85 -1.75

    static Vector2 LimitsY = new Vector2(-4.85f, -1.75f);

    // Start is called before the first frame update
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int health = 100;
    [SerializeField] private float verticalSpeed = 1.5f;
    [SerializeField] private float horizontalSpeed = 3f;

    // Define the control keys for both players
    [SerializeField] private int playerNumber = 1;
    private string horizontalKey;
    private string verticalKey;
    private string shootKey;
    private string punchKey;
    public static Player Instance;

    public VectorValue startingPosition;
    Vector2 vector_position;

    

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
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

    Vector2 control;
    // Update is called once per frame
    void Update()
    {
        control = new Vector2(Input.GetAxisRaw(horizontalKey), Input.GetAxisRaw(verticalKey));
        if (control.x != 0)
        {
            transform.rotation = Quaternion.Euler(0, control.x > 0 ? 0 : 180, 0);
        }
        animator.SetBool("isWalking", control.magnitude != 0);
        animator.SetBool("isShooting", Input.GetButtonDown(shootKey));
        animator.SetBool("isPunching", Input.GetAxisRaw(punchKey) != 0);
        rb.velocity = new Vector2(control.x * horizontalSpeed, control.y * verticalSpeed);
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, LimitsY.x, LimitsY.y));

        if (health <= 0)
        {
            SceneManager.LoadScene(5);
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
}
