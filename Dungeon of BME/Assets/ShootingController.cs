using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCam;
    public GameObject bullet;
    public Transform bulletTransform;
    public float timer;
    public float fireRate;

    private bool canFire=true;

    public Animator animator;
    private float rotZ;


    




    // Start is called before the first frame update
    void Start()
    {
       // Cursor.visible= false;
       // Cursor.lockState=CursorLockMode.Locked;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
    }


    // Update is called once per frame
    private void FixedUpdate(){

        
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if(!canFire){
            timer += Time.deltaTime;
            if(timer>fireRate){
                canFire=true;
                timer=0;
            }
        
        }
        
        if (Input.GetKey(KeyCode.Q )&& canFire){
            canFire=false;
            ShootArrow(0);
            animator.SetTrigger("Shoot");
        }

        //special bullet 
        if (Input.GetKey(KeyCode.R )&& canFire){
            canFire=false;
            
            ShootArrow(30);
            ShootArrow(0);
            ShootArrow(-30);

            animator.SetTrigger("Shoot");
        }

       
       

    }

    public void reload(){
        canFire=true;
    }

    private void ShootArrow(float dif){
        Vector3 MousePosDiff = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        Vector3 direction = MousePosDiff - transform.position;
        Vector3 rotation = transform.position - MousePosDiff;

        GameObject clone = Instantiate(bullet, bulletTransform.position, Quaternion.Euler(0,0,rotZ) );
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            

        float rot= Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;   

        Vector2 arrowDir= new Vector2( direction.x , direction.y ).normalized * 1 ;
        
        rb.velocity = Quaternion.Euler(0,0,dif) * arrowDir;
       //rb.velocity =  arrowDir * Quaternion.Euler(0,0,rot + 180 +dif); Ezt a rakos szart....
        print("arrow"+direction.x +" "+ direction.y);

        clone.transform.rotation = Quaternion.Euler(0,0,rot + 180 +dif);

    }


     


}
