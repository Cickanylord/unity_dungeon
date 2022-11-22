using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamage
{

    //movement params 
    Vector2 movementInput;
    Rigidbody2D rb;
    public float movementSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;

    bool isMoving = false;
    bool IsMoving{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }


/*
    // params for casting 
    public ContactFilter2D movmentFilter;
    List<RaycastHit2D> castCollisions=new List<RaycastHit2D>();
    
    public float collisionOffset = 0.05f;
    */

    SpriteRenderer spriteRenderer;

    //Animation
    Animator animator;
    

    //attack
    public SwordAttack swordAttack;

    //movement 
    bool canMove=true;

    //health 
    public float health=10;

    public float Health{
        set{
            health =value;
            print(health);
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

    //initialize player components
    void Start(){
        rb=GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    private void FixedUpdate(){
        // move the player, if there is input 
        if(canMove && movementInput!=Vector2.zero){
            rb.velocity = Vector2.ClampMagnitude(movementInput * movementSpeed * Time.fixedDeltaTime, maxSpeed);

            if(movementInput.x < 0){
                spriteRenderer.flipX=true;
            }   
            else if(movementInput.x > 0){
                spriteRenderer.flipX=false;
            }
            IsMoving=true;
        }
        //stops the player 
        else{
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving=false;
        }
    }

    //plays death animation 
    private void Defeated(){
        LockMovement();
        animator.SetTrigger("Defeated");
    }
    //After death animation finished deletes the object 
    private void RemoveEnemy(){
        Destroy(gameObject);
    }

    //gets player position when moving 
    void OnMove(InputValue movmentValues ){
        movementInput=movmentValues.Get<Vector2>();
    }

    //Attack functions
    //plays attack animation  
    void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    //triggered by the animation, decide which way the player attacks  
    public void SwordAttack(){
        LockMovement();
        if(spriteRenderer.flipX==true){
            swordAttack.AttackLeft();
        }
        else{
            swordAttack.AttackRight();
        }
    }
    //Triggerd at the end of attack animation 
    public void EndSwordAttck(){
        UNLockMovement();
        swordAttack.AttackStop();

    }
    //Locks the player movement while attacking 
    public void LockMovement(){
       canMove=false;

    }
     //Unlocks the player movement after attacking 
    public void UNLockMovement(){
        canMove=true;
    }


    //Get damage functions 
    public void onHit(float damage, Vector2 knockback){
        Health-=damage;
        rb.AddForce(knockback);
    }


    public void onHit(float damage){
        print("hit");
        Health-=damage;
    }
}
