using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage 
{
    public float Health{ set; get;}
    public void onHit(float damage, Vector2 knockback); 
    public void onHit(float damage); 
}
