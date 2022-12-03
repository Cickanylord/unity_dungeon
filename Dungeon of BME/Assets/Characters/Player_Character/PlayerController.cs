 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IDamage
{
    GameObject gameController;

    //movement params 
    Vector2 movementInput;

    public Vector2 MovementInput{
        get{
            return movementInput;
        }
    }
    public float movementSpeed = 150f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    bool dead=false;
    bool victory = false;
    SpriteRenderer spriteRenderer;

    //Animation
    Animator animator;

    Rigidbody2D rb;
    Collider2D col;
    GameObject[] enemies;
    public GameObject inventory;

    //attack
    public SwordAttack swordAttack;
    public GameObject sword;
    Vector2 swordRightAttackOffset;
    public float timer;

    //health 
    public float maxHealth { get; set;}
    public float health=10;
    public ValueBar healthbar;

    public float Health{
        set{
            if(value >= maxHealth){
                health = maxHealth;
            }
            else{
                health = value;
            }
            print(health);
            healthbar.SetValue(health);
            //print(health);
                if(health<=0){
                    print("player "+ health);
                    Defeated();
                }     
        }
        get{
                
            return health;
        }
    }


    //mana
    public ValueBar manabar;
    public float manaRegenRate = 1;
    private int maxMana; 
    public int mana = 10;
    public int manaIncremention = 1;
    int manaIncrementionPause;
    public int Mana{
        set{
            mana = value;
            manabar.SetValue(Mana);
        }

        get{
            return mana;
        }
    } 

    //movement 
    bool canMove=true;
    bool isMoving = false;
    bool IsMoving{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    //initialize player components
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator =GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        swordRightAttackOffset=sword.transform.localPosition;
        //print("original: "+sword.transform.localPosition);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxMana = Mana;
        maxHealth = Health;
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update(){
        
    }

    //movement of player
    private void FixedUpdate(){

        //canMove = !gameController.GetComponent<GameController>().Pause;

        if(!dead){
            // move the player, if there is input 
            if(canMove && movementInput!=Vector2.zero){
                rb.velocity = Vector2.ClampMagnitude(movementInput * movementSpeed * Time.fixedDeltaTime, maxSpeed);

                if(movementInput.x < 0){
                    spriteRenderer.flipX=true;
                    //sword placement
                    sword.transform.rotation = Quaternion.Euler(0,0,30);
                    sword.transform.localPosition = new Vector3(swordRightAttackOffset.x*-1, swordRightAttackOffset.y);
                    //print("right: "+sword.transform.localPosition);
                }   
                else if(movementInput.x > 0){
                    spriteRenderer.flipX=false;
                    //sword placement
                    sword.transform.rotation = Quaternion.Euler(0,0,-30);
                    sword.transform.localPosition = swordRightAttackOffset;
                    //print("left: "+sword.transform.localPosition);
                }
                IsMoving=true;
            }
            //stops the player 
            else{
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                IsMoving=false;
            }

            //mana regen
            if(Mana < maxMana){
                //print("kisebb");
                timer += Time.deltaTime;
                if(timer>manaRegenRate){
                    timer=0;
                    Mana += manaIncremention;
                }
            }
        }

        if((dead || victory) && Input.GetKey(KeyCode.Q )){
            ResPawn();
        }


    }


    //plays death animation 
    private void Defeated(){
        LockMovement();
        col.enabled= false;
        inventory.SetActive(false);
        dead = true;
        animator.SetTrigger("Defeated");
        //TODO make death scene apeer 
        GameObject.FindGameObjectWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().text = "Game Over!";
        GameObject.FindGameObjectWithTag("Restart").GetComponent<UnityEngine.UI.Text>().text = "Press Q to restart";
    }

    public void Pause(){
        LockMovement();
        col.enabled= false;
        manaIncrementionPause = manaIncremention;
        manaIncremention = 0;
    }
   
    public void Resume(){
        UNLockMovement();
        col.enabled= true;
        manaIncremention = manaIncrementionPause;
    }

    public void Victory(){
        victory = true;
    }
 

    //gets player position when moving 
    void OnMove(InputValue movmentValues ){
        movementInput=movmentValues.Get<Vector2>();
    }

    //Attack functions
    //plays attack animation  
    void OnFire(){
        if(sword.activeSelf){
            animator.SetTrigger("swordAttack");
            sword.GetComponent<SpriteRenderer>().enabled=false;
        }
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
        sword.GetComponent<SpriteRenderer>().enabled=true;
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
        if(Health>0)
            rb.AddForce(knockback);
    }


    public void onHit(float damage){
        print("hit");
        Health-=damage;
    }

    public void ResPawn(){

        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        //TODO make death scene disapeer 
        /*GameObject.FindGameObjectWithTag("GameOver").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.FindGameObjectWithTag("Restart").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.FindGameObjectWithTag("Victory").GetComponent<UnityEngine.UI.Text>().text = "";
        dead = false;
        transform.position = new Vector3(0,0,0);
        Health = maxHealth;
        Mana = maxMana;
        animator.SetTrigger("Respawn");
        canMove=true;
        col.enabled= true;
        inventory.SetActive(true);

        foreach (var enemy in enemies){
            IDamage enemyObject = (IDamage) enemy.GetComponent<IDamage>();
            enemyObject.ResPawn();
            print("reses");
        }*/

    }

    

}
