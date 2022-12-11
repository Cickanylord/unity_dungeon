using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour{
    private Vector3 mousePos;
    private Camera mainCam;
    public Rigidbody2D rb;
    public float speed =500f;

    public float damage = 3;

    public string targetTag="Enemy";

    public float knockbackForce=500f;

    // Start is called before the first frame update
    void Start(){
        /*
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        float rot= Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rb.velocity = new Vector2( direction.x, direction.y).normalized * speed;
        transform.rotation = Quaternion.Euler(0,0,rot+180);
         */
    }


    private void OnTriggerEnter2D(Collider2D other){
        IDamage enemyObject = (IDamage) other.GetComponent<IDamage>();
        if(other.tag==targetTag){
            
            //knockback
            Vector3 parentpos = gameObject.GetComponentInParent<Transform>().position;
            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentpos).normalized;

            enemyObject.onHit(damage,direction*knockbackForce);
            Destroy(this.gameObject);
        }
        if(other.tag == "HitBox"){
            Destroy(this.gameObject);
        }

        if(other.tag=="Wall"){
            Destroy(this.gameObject);
        }
        print(other.tag);
        
    }

    private void OnCollisionEnter2D(Collision2D other){
            if(other.collider.tag == "HitBox"){
            Destroy(this.gameObject);
            print("hitbox");
        }
        print("hitbox");
    }


    // Update is called once per frame
    void Update(){
        
    }
}
