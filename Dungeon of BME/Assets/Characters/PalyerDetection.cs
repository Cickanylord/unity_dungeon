using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyerDetection : MonoBehaviour
{
    public string tagTarget = "Player";
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag==tagTarget){
            detectedObjs.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag==tagTarget){
            detectedObjs.Remove(other);
        }
    }

}
