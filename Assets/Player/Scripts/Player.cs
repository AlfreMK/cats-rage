using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    // -4.85 -1.75

    static Vector2 LimitsY = new Vector2(-4.85f, -1.75f);

    // Start is called before the first frame update
    [SerializeField] private float verticalSpeed = 0.3f;
    [SerializeField] private float horizontalSpeed = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    Vector2 control;
    // Update is called once per frame
    void Update()
    {
        control = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (control.x != 0)
        {
            transform.rotation = Quaternion.Euler(0, control.x > 0 ? 0 : 180, 0);
        }
        animator.SetBool("isWalking", control.magnitude != 0);
        animator.SetBool("isShooting", Input.GetKeyDown(KeyCode.V));
        animator.SetBool("isPunching", Input.GetKeyDown(KeyCode.G));

        rb.velocity = new Vector2(control.x * horizontalSpeed, control.y * verticalSpeed);
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, LimitsY.x, LimitsY.y));
        
    }
}
