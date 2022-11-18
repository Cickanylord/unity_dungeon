using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;


    // params for casting 
    public ContactFilter2D movmentFilter;
    List<RaycastHit2D> castCollisions=new List<RaycastHit2D>();
    public float movementSpeed=1f;
    public float collisionOffset = 0.05f;
    SpriteRenderer spriteRenderer;

    //Animation
    Animator animator;
    bool canMove=true;

    //attack
    public SwordAttack swordAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator =GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate(){

        if(canMove){
            if(movementInput!=Vector2.zero){

            if(tryMove(movementInput)){ animator.SetBool("isMoving", true);}
            else{
                if(tryMove(new Vector2(movementInput.x,0))){
                     animator.SetBool("isMoving", true);
                }

                else if( tryMove(new Vector2(0,movementInput.y))){
                     animator.SetBool("isMoving", true);
                }
            }
        }
        else{
            animator.SetBool("isMoving", false);
        }

        if(movementInput.x < 0){
            spriteRenderer.flipX=true;
        }   
        else if(movementInput.x > 0){
            spriteRenderer.flipX=false;
        }
        }
    
    }

    private bool tryMove(Vector2 direction){
        if(direction!=Vector2.zero){

            int count=rb.Cast(direction,movmentFilter,castCollisions,movementSpeed*Time.fixedDeltaTime+collisionOffset);
            if(count==0){
                rb.MovePosition(rb.position + direction  * movementSpeed*Time.fixedDeltaTime);
                //animator.SetBool("isMoving", true);
                return true;
            }
        
            else{
                return false;
            }
        }
        else{
            return false;
        }

    }

    void OnMove(InputValue movmentValues ){
        movementInput=movmentValues.Get<Vector2>();
    }

     void OnFire(){
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack(){
        LockMovement();
        if(spriteRenderer.flipX==true){
            swordAttack.AttackLeft();
        }
        else{
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttck(){
        UNLockMovement();
        swordAttack.AttackStop();

    }

    public void LockMovement(){
        canMove=false;

    }

     public void UNLockMovement(){
        canMove=true;
    }
}
