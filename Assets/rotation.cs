using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    // Start is called before the first frame update    public float rotationSpeed = 30f; // Velocidad de rotación del meteorito

    public float rotationSpeed = 60f;
    public Rigidbody2D rb;
    public GameObject explosionPrefab;
    private string shadowObjName;


    void Update()
    {
        // Rotar continuamente el meteorito
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    public void Spawn(Vector3 meteorPosition, Vector3 shadowPosition, GameObject shadow){
        GameObject met = Instantiate(gameObject, meteorPosition, Quaternion.identity);
        met.GetComponent<rotation>().SetName(shadow.name);
        float timeFalling = 4.0f;
        float velocityX = (0.4f)/timeFalling;
        float velocityY = (meteorPosition.y-shadowPosition.y)/timeFalling;
        Rigidbody2D rb2 = met.GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(-velocityX, -velocityY);
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
	    if (hitInfo.gameObject.name == shadowObjName){
            Instantiate(explosionPrefab, transform.position + (explosionPrefab.transform.up), Quaternion.identity);
            hitInfo.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void SetName(string name){
        shadowObjName = name;
    }


}
