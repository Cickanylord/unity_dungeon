using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    GameObject gameController;
    //basic params 
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Vector3 originPos;


    //Attack player params
    public float knockbackForce=500f;
    public float damage=3;
    //movement params 
    public PalyerDetection detection;
    public float movementSpeed = 500f;  

    // health 
    private float maxHealth;
    public float health=1;
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

    //decides if character is moving 
    bool isMoving = false;
    bool IsMoving{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }


        // Start is called before the first frame update
    void Start(){
        maxHealth = Health;
        originPos = transform.position;
        animator=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void FixedUpdate(){
        //if ther is a player the enemy goes towards it 
        if(detection.detectedObjs.Count>0 && !gameController.GetComponent<GameController>().pause){
            Collider2D detectedObject = detection.detectedObjs[0];

            Vector2 directionToPlayer = (detectedObject.transform.localPosition - transform.localPosition).normalized;
            rb.AddForce(directionToPlayer * movementSpeed * Time.deltaTime);


            //flips enemy weapon
            if(directionToPlayer.x < 0){
                spriteRenderer.flipX=true;
            }   
            else if(directionToPlayer.x > 0){
                spriteRenderer.flipX=false;
            }

            //if there is force applied the character is moving 
            IsMoving=true;
        }
        else{
            IsMoving=false;
        }
    }

    //when the enemy gets defeated sets trigger for animation 
    private void Defeated(){
        rb.velocity=new Vector2(0,0);
        animator.SetTrigger("Defeated");
    }

    private void RemoveEnemy(){
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }


    public void ResPawn(){
        animator.SetTrigger("Respawn");
        print("reseted");
        gameObject.SetActive(true);
        maxHealth = Health;
        transform.position = originPos;
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
        //print("player_hit");

        IDamage enemyObject = (IDamage) other.collider.GetComponent<IDamage>();

        if(enemyObject != null && other.collider.tag == "Player"){
            //print(other.collider.tag);
            //knockback
            Vector3 parentpos = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentpos).normalized;
            Vector2 knockback = direction * knockbackForce;

            enemyObject.onHit(damage, knockback);
        }

    }

}

