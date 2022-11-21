using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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


    // params for casting 
    public ContactFilter2D movmentFilter;
    List<RaycastHit2D> castCollisions=new List<RaycastHit2D>();
    
    public float collisionOffset = 0.05f;
    SpriteRenderer spriteRenderer;

    //Animation
    Animator animator;
    

    //attack
    public SwordAttack swordAttack;

    bool canMove=true;

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
            if(canMove && movementInput!=Vector2.zero){
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * movementSpeed * Time.deltaTime), maxSpeed);


            if(movementInput.x < 0){
                spriteRenderer.flipX=true;
            }   
            else if(movementInput.x > 0){
                spriteRenderer.flipX=false;
            }
            IsMoving=true;
        }
        else{
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            IsMoving=false;
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
