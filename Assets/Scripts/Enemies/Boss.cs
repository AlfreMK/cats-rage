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
    [SerializeField] public GameObject[] woolShadows;

    [SerializeField] public GameObject meteorPrefab;




    private Animator animator;
    private Rigidbody2D rb;
    // private bool isMovingUp = true;
    private static readonly int _animationIdle = Animator.StringToHash("Idle");
    private static readonly int _animationAttack = Animator.StringToHash("Attack");
    private static readonly int _animationDefeat = Animator.StringToHash("Defeat");
    private bool firstPhase = true;
    private bool secondPhase = false;
    private bool isSpawningMeteors = false;

    
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
        if (shield.activeSelf){
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            StartCoroutine(ResetColor());
            return;
        }
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
                        if (!isSpawningMeteors)
                            {
                                isSpawningMeteors = true;
                                StartCoroutine(SpawnMeteors());
                            }
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

    IEnumerator SpawnMeteors()
    {
        float duration = 25.0f;
        float timer = 0f;

        while (timer < duration)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

            // Choose a random shadow that is not currently active
            GameObject randomShadow = GetRandomInactiveShadow();
            if (randomShadow != null)
            {
                // Spawn a meteor at a specific Y position (replace 'yPosition' with the desired Y coordinate)
                InstantiateMeteor(randomShadow.transform.position.x + 0.4f, randomShadow.transform.position.y + 30, randomShadow);
                randomShadow.SetActive(true);
                StartCoroutine(ShadowAnimation(randomShadow));
            }

            timer += 1.0f;
        }

        isSpawningMeteors = false;
    }

    GameObject GetRandomInactiveShadow()
    {
        // Filter out currently active shadows
        GameObject[] inactiveShadows = System.Array.FindAll(woolShadows, shadow => !shadow.activeSelf);

        // Return a random inactive shadow
        return inactiveShadows.Length > 0 ? inactiveShadows[Random.Range(0, inactiveShadows.Length)] : null;
    }

    void InstantiateMeteor(float xPosition, float yPosition, GameObject shadow)
    {
        // Instantiate the meteor at the specified position
        // Replace 'meteorPrefab' with the actual meteor prefab you want to use
        rotation meteorPrefabScript = meteorPrefab.GetComponent<rotation>();
        meteorPrefabScript.Spawn(new Vector3(xPosition, yPosition, 0), transform.position, shadow);
    }

    IEnumerator ShadowAnimation(GameObject shadow)
    {
        float time = 0;
        while (time < 3.0f)
        {
            shadow.transform.localScale = new Vector3(0.1f, 0.1f, 1) * (time / 3.0f);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
