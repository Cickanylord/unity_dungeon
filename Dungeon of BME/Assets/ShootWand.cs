using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWand : MonoBehaviour
{
    public PlayerController playerController;
    private Vector3 mousePos;
    private Camera mainCam;
    public GameObject bullet;
    public Transform bulletTransform;
    public float timer;
    public float fireRate;
    Vector2 wandRightAttackOffset;
    Vector2 movementInput;
    public float damage;
    private float manaCost = 2f;
    public float ManaCost{
        get {
            return manaCost;
        }
        set {
            if(value <= 0f){
                manaCost = 0f;
            }
            else{
                manaCost = value;
            }
        }
    }
    private float specialManaCost = 5f;
    public float SpecialManaCost{
        get {
            return specialManaCost;
        }
        set {
            if(value <= 0f){
                specialManaCost = 0f;
            }
            else{
                specialManaCost = value;
            }
        }
    }
    public AudioSource projectileSound;
    private GameObject gameController;


    private bool canFire=true;

    //public Animator animator;
    private float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();  
        wandRightAttackOffset = transform.localPosition;
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
        private void Update(){

        movementInput = playerController.MovementInput;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0,0,rotZ);

        if(!canFire){
            timer += Time.deltaTime;
            if(timer>fireRate){
                canFire=true;
                timer=0;
            }
        }
        
        if (Input.GetKey(KeyCode.Q )&& canFire && playerController.Mana >= ManaCost && !gameController.GetComponent<GameController>().Pause){
            playerController.Mana -= ManaCost;
            canFire =false;
            ShootArrow(0);

            //animator.SetTrigger("Shoot");
        }

        //special bullet 
        if (Input.GetKey(KeyCode.R )&& canFire && playerController.Mana >= SpecialManaCost && !gameController.GetComponent<GameController>().Pause){
            canFire=false;
            playerController.Mana -= SpecialManaCost;
            ShootArrow(20);
            ShootArrow(0);
            ShootArrow(-20);

            //animator.SetTrigger("Shoot");
        }

        if(movementInput.x < 0){
        transform.rotation = Quaternion.Euler(0,0,20);
        transform.localPosition = new Vector3(wandRightAttackOffset.x*-1, wandRightAttackOffset.y);
        }

        else if(movementInput.x > 0){
            transform.rotation = Quaternion.Euler(0,0,-20);
            transform.localPosition = wandRightAttackOffset;
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
        clone.GetComponent<BulletMover>().damage = damage;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            

        float rot= Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;   

        Vector2 arrowDir= new Vector2( direction.x , direction.y ).normalized * 1 ;
        
        rb.velocity = Quaternion.Euler(0,0,dif) * arrowDir;
       //rb.velocity =  arrowDir * Quaternion.Euler(0,0,rot + 180 +dif); Ezt a rakos szart....
        print("arrow"+direction.x +" "+ direction.y);

        clone.transform.rotation = Quaternion.Euler(0,0,rot + 180 +dif);
        projectileSound.Play();

    }
}
