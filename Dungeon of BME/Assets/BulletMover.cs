using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour{
    private Vector3 mousePos;
    private Camera mainCam;
    public Rigidbody2D rb;
    public float speed =500f;

    // Start is called before the first frame update
    void Start(){
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
        mousePos = mainCam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2( direction.x, direction.y).normalized * speed;
        float rot= Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot+180);
    }


    private void OnTriggerEnter2D(Collider2D other){
        print("HIT");
        if(other.tag=="Enemy"){
            Destroy(other.gameObject);
        }
        
    }


    // Update is called once per frame
    void Update(){
        
    }
}
