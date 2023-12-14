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

    private bool thirdPhase = false;
    private bool isMovingUp = false;

    private float initialPosY;

    private bool isDefeated = false;

    private bool isSpawingMeteors = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void toogleBool(bool b){
        b = !b;
    }

    IEnumerator Move(){
        if (isDefeated){
            yield break;
        }
        while (secondPhase){
            yield break;
        } 
        while (thirdPhase || firstPhase){
            if (transform.position.y >= -3.3f){
                isMovingUp = false;
            }
            else if (transform.position.y <= -4.3f){
                isMovingUp = true;
            }
            else {
                if (Random.value >= 0.5)
                {
                    isMovingUp = true;
                }
                else{
                    isMovingUp = false;
                }
            }
            int[] numbers = new int[3] {1, 2, 3};
            int myRandom = numbers[Random.Range(0,3)];
            int iter = 0;
            while (iter < myRandom){
                if (isMovingUp){
                    float time = 0;
                    float nextY = transform.position.y + 0.1f;
                    if (nextY >= -3.3f){
                        nextY = -3.3f;
                    }
                    while (time < 1)
                    {
                        time += Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, nextY, time), transform.position.z);
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                else{
                    float time = 0;
                    float nextY = transform.position.y - 0.1f;
                    if (nextY <= -4.3f){
                        nextY = -4.3f;
                    }
                    while (time < 1)
                    {
                        time += Time.deltaTime;
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, nextY, time), transform.position.z);
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                if (secondPhase){
                    iter = 3;
                }
                iter++;
            }
        }
    }


    public void TakeDamage(int damage) {
        if (shield.activeSelf){
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            StartCoroutine(ResetColor());
            return;
        }
        lifeBoss -= damage;
        if (lifeBoss <= 0) {
            lifeBoss = 0;
            StopCoroutine("SpawnMeteors");
            StopCoroutine("EnemyBehaviour");
            StopCoroutine("Move");
            isDefeated = true;
            Destroy(consistencyWall);
            Destroy(fire);
            SetAnimationState(_animationDefeat);
            Destroy(gameObject, 2.3f);
        }
        if (lifeBoss <= 700 && firstPhase){
            StartCoroutine(RedoPos());
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
            if (isDefeated){
                yield break;
            }
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
                        if (!isSpawingMeteors){
                            StartCoroutine("SpawnMeteors");
                        }
                        yield return new WaitForSeconds(1.0f);
                    }
                }
            }

            else if (thirdPhase){
                SetAnimationState(_animationAttack);
                yield return new WaitForSeconds(1.0f);
            }
            else{
                SetAnimationState(_animationIdle);
                yield return new WaitForSeconds(1.0f);
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
        else if (thirdPhase && shield.activeSelf){
            shield.SetActive(false);
        }
        else if (thirdPhase && !shield.activeSelf){
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

    }

    void makeIdle(){
        SetAnimationState(_animationIdle);
    }

    public void startAttacking(){
        initialPosY = transform.position.y;
        StartCoroutine("EnemyBehaviour");
        StartCoroutine("Move");
    }

    IEnumerator SpawnMeteors()
    {
        isSpawingMeteors = true;
        StartCoroutine("StartLastPhase");
        

        while (!isDefeated)
        {
            yield return new WaitForSeconds(Random.Range(0.7f, 1.8f));

            // Choose a random shadow that is not currently active
            GameObject randomShadow = GetRandomInactiveShadow();
            if (randomShadow != null)
            {
                // Spawn a meteor at a specific Y position (replace 'yPosition' with the desired Y coordinate)
                InstantiateMeteor(randomShadow.transform.position.x + 0.4f, randomShadow.transform.position.y + 30, randomShadow);
                randomShadow.SetActive(true);
                StartCoroutine(ShadowAnimation(randomShadow));
            }

        }
    }

    IEnumerator StartLastPhase(){
        yield return new WaitForSeconds(20.0f);
        shield.SetActive(false);
        secondPhase = false;
        thirdPhase = true;
        StartCoroutine("Move");
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
    IEnumerator RedoPos()
    {
        float time = 0;
        float nextY = initialPosY;
        while (time < 1)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, nextY, time), transform.position.z);
            yield return null;
        }
    }
}
