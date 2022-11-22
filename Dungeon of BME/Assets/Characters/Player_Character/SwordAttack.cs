using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public float damage=3;
    public float knockbackForce=500f;

    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AttackLeft(){
        //print("attack left");
        swordCollider.enabled=true;
        transform.localPosition = new Vector3(rightAttackOffset.x*-1, rightAttackOffset.y);
    }

    public void AttackRight(){
        //print("attack right");
         transform.localPosition=rightAttackOffset;
        swordCollider.enabled=true;
    }

    public void AttackStop(){
        swordCollider.enabled=false;
    }


    private void OnTriggerEnter2D(Collider2D other){
        IDamage enemyObject = (IDamage) other.GetComponent<IDamage>();

        if(enemyObject != null){
            print(other.tag);
            //knockback
            Vector3 parentpos = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (other.gameObject.transform.position - parentpos).normalized;
            Vector2 knockback = direction * knockbackForce;

            enemyObject.onHit(damage, knockback);
        }

    }

  


}
