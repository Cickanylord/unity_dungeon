using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamage
{
    //basic params 
    public AudioSource rangedEnemyDeath;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Vector2 originlPos;
    GameController gameController;
    bool alive = true;

    // health 
    private float maxHealth;
    public float health=20;
    //Attack player params
    public float knockbackForce=500f;
    public float damage=3;
    public float fireRate = 1f;
    private float nextFire = 0.0f;


    //movement params 
    public PalyerDetection aim;
    public PalyerDetection move;

    public float movementSpeed = 500f;  
    public GameObject bullet;

    public string targetTag="Player";
   


    bool isMoving = false;
    bool IsMoving{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    void FixedUpdate(){
        
        if(move.detectedObjs.Count>0 && alive/*&& !gameController.GetComponent<GameController>().pause*/){
            Collider2D detectedObject = move.detectedObjs[0];

            Vector2 directionToPlayer = (detectedObject.transform.position - transform.position).normalized;

            float rotZ = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            if(aim.detectedObjs.Count>0){
                if(Time.time > nextFire){

                    for (int i = 0; i <= 350; i+=20){
                        ShootArrowAtPlayer(directionToPlayer,i);
                    }
                    
                }
                if(directionToPlayer.x < 0){
                    spriteRenderer.flipX=true;
                }   
                else if(directionToPlayer.x > 0){
                    spriteRenderer.flipX=false;
                }

                
            }
       
        
            if(move.detectedObjs.Count>0 && aim.detectedObjs.Count == 0){
                rb.AddForce(directionToPlayer * movementSpeed * Time.deltaTime);
                IsMoving=true;
            }
            else{
                IsMoving=false;
            }
        }

        else{
            IsMoving=false;
        }
    }



    public float Health{
        set{
            health =value;
           
            print("hit");
            if(health<=0){
                Defeated();
            }else{
                //animator.SetTrigger("Demaged");
            }
            
        }
        get{
            
            return health;
        }
    }

    private void Defeated(){
        gameController.EnemyDies();
        animator.SetTrigger("Defeated");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //rangedEnemyDeath.Play();
        alive = false;
    }

    private void RemoveEnemy(){
        //Destroy(gameObject); 
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        maxHealth = Health;
        originlPos = transform.position;
        gameController = (GameController) GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Idle(){
        animator.SetTrigger("Idle");
    }

    public void onHit(float damage, Vector2 knockback){
        Health-=damage;
        if(Health>0)
            rb.AddForce(knockback);
    }


    public void onHit(float damage){
        //print("hit");
        Health-=damage;
    }



    void OnCollisionEnter2D(Collision2D other){
       // print("player_hit");
        if(alive){
            IDamage enemyObject = (IDamage) other.collider.GetComponent<IDamage>();

            if(enemyObject != null){
                if(other.collider.tag==targetTag){
                print(other.collider.tag);
                //knockback
                Vector3 parentpos = gameObject.GetComponentInParent<Transform>().position;
                Vector2 direction = (Vector2) (other.gameObject.transform.position - parentpos).normalized;
                Vector2 knockback = direction * knockbackForce;

                enemyObject.onHit(damage, knockback);
                }
            }
        }

    }

    private void ShootArrowAtPlayer(Vector2 direction, float degree){
        
        nextFire = Time.time + fireRate;
        GameObject clone = Instantiate(bullet,transform.position, transform.rotation);
        BulletMover bulletMover= clone.GetComponent<BulletMover>();
        bulletMover.targetTag = targetTag;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        Vector2 arrowDir = new Vector2( direction.x , direction.y ).normalized * 1 ;
        rb.velocity = Quaternion.Euler(0,0,degree) * arrowDir;
        float rotZ = Mathf.Atan2(arrowDir.y, arrowDir.x) * Mathf.Rad2Deg;



        
        clone.transform.rotation = Quaternion.Euler(0,0,rotZ + degree);
        
    }


    public void ResPawn(){
        animator.SetTrigger("Respawn");
        print("reseted");
        gameObject.SetActive(true);
        maxHealth = Health;
        transform.position = originlPos;
        alive = true;
    }



}

