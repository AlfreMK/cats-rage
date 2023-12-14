using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour, CanTakeDamage
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] public int lifeBoss = 1000;
    [SerializeField] public GameObject consistencyWall;
    [SerializeField] public GameObject fire;
    [SerializeField] public GameObject shield;
    private Animator animator;
    private Rigidbody2D rb;
    // private bool isMovingUp = true;
    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationDefeat = Animator.StringToHash("Defeat");
    private bool firstPhase = true;
    private bool secondPhase = false;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void toogleBool(bool b){
        b = !b;
    }

    // IEnumerator Move(){
        // TODO: Move the boss
    // }




    public void TakeDamage(int damage) {
        lifeBoss -= damage;
        if (lifeBoss <= 0) {
            lifeBoss = 0;
            SetAnimationState(_animationDefeat);
            Destroy(gameObject, 2.3f);
        }
        if (lifeBoss <= 700 && firstPhase){
            firstPhase = false;
            secondPhase = true;
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        StartCoroutine(ResetColor());
    }
    
    public IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    void SetAnimationState(int state)
    {
        animator.CrossFade(state, 0, 0);
    }

     IEnumerator EnemyBehaviour()
    {
        while (true)
        {
            // yield return Move();
            if (firstPhase){
                SetAnimationState(_animationAttack);
                yield return new WaitForSeconds(2.0f);
            }
            else if (secondPhase){
                if (!fire.activeSelf){
                    SetAnimationState(_animationAttack);
                    yield return new WaitForSeconds(1.0f);
                }
                else{
                    if (!shield.activeSelf){
                        SetAnimationState(_animationAttack);
                        yield return new WaitForSeconds(1.0f);
                    }
                    else{
                        yield return new WaitForSeconds(1.0f);
                    }
                }
            }

        }
    }

    void makeBullet(){
        if (firstPhase){
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else if (secondPhase){
            if (!fire.activeSelf){
                consistencyWall.SetActive(true);
                fire.SetActive(true);   
            }
            else{
                shield.SetActive(true);
            }
        }
    }

    void makeIdle(){
        SetAnimationState(_animationIdle);
    }

    public void startAttacking(){
        StartCoroutine(EnemyBehaviour());
    }
}
