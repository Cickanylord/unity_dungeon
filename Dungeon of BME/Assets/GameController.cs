using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private bool pause;
    public bool Pause{
        set{
            pause = value;
            if (pause){
               playerContorller.Pause(); 
            }
            if(!pause){
                playerContorller.Resume(); 
            }
        }
        get{
        return pause;
        }
    }
    PlayerController playerContorller;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerContorller = (PlayerController) player.GetComponent<PlayerController>();
        pause = false;
    }

    void DisableProjectiles(){

    }
    
    void EnableProjectiles(){

    }

    public void EnemyDies(){
        playerContorller.Health++;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
