using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float health=1;

    Rigidbody2D rb;


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Idle(){
        animator.SetTrigger("Idle");
    }





    public void KonckBack(Vector2 knockback)
    {
        rb.AddForce(knockback);
    }

    void onHit(float demage){
        print("hit");
        Health-=demage;
    }
}
