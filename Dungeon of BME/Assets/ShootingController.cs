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


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible= false;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
    }


    // Update is called once per frame
    private void FixedUpdate(){

        
        mousePos = mainCam.ScreenToViewportPoint(Input.mousePosition);

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
            Instantiate(bullet, bulletTransform.position, Quaternion.identity );
        }

       
       

    }



}
