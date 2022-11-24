using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, IDamage
{
    //basic params 
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    // health 
    public float health=1;
    //Attack player params
    public float knockbackForce=500f;
    public float damage=3;
    //movement params 
    public PalyerDetection detection;
    public float movementSpeed = 500f;  
    public GameObject bullet;


    bool isMoving = false;
    bool IsMoving{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    void FixedUpdate(){
        if(detection.detectedObjs.Count>0){
            Collider2D detectedObject = detection.detectedObjs[0];
            Vector2 directionToPlayer = (detectedObject.transform.position - transform.position).normalized;
            //rb.AddForce(directionToPlayer * movementSpeed * Time.deltaTime);
            ShootArrowAtPlayer(directionToPlayer);

            if(directionToPlayer.x < 0){
                spriteRenderer.flipX=true;
            }   
            else if(directionToPlayer.x > 0){
                spriteRenderer.flipX=false;
            }

            IsMoving=true;
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
                 animator.SetTrigger("Demaged");
            }
            
        }
        get{
            
            return health;
        }
    }

    private void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    private void RemoveEnemy(){
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
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
        rb.AddForce(knockback);
    }


    public void onHit(float damage){
        print("hit");
        Health-=damage;
    }



    void OnCollisionEnter2D(Collision2D other){
        print("player_hit");

        IDamage enemyObject = (IDamage) other.collider.GetComponent<IDamage>();

        if(enemyObject != null){
            print(other.collider.tag);
            //knockback
            Vector3 parentpos = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentpos).normalized;
            Vector2 knockback = direction * knockbackForce;

            enemyObject.onHit(damage, knockback);
        }

    }

    private void ShootArrowAtPlayer(Vector2 direction){
        GameObject clone = Instantiate(bullet,transform.parent) ;
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        Vector2 arrowDir = new Vector2( direction.x , direction.y ).normalized * 1 ;
        rb.velocity = arrowDir;
        //clone.transform.rotation = Quaternion.Euler(0,0,directio + 180);

    }
}

