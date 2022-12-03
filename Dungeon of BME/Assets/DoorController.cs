using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public bool isOpen;
    private Animator animator;
    public string keyTag;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        bool hasKey = false;
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        for (int i = 0; i < inventory.transform.childCount; i++){
            if(inventory.transform.GetChild(i).gameObject.CompareTag(keyTag)){
                hasKey = true;
            }
        }
        if(hasKey){
            isOpen = true;
            animator.SetBool("isOpen", isOpen);
        }
    }

    public void TeleportPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnpoint = GameObject.FindGameObjectWithTag("SpawnFinal");
        player.GetComponent<PlayerController>().Health = player.GetComponent<PlayerController>().maxHealth;
        if(keyTag =="GreenKey"){
            spawnpoint = GameObject.FindGameObjectWithTag("SpawnBlue");
        }
        if(keyTag == "BlueKey"){
            spawnpoint = GameObject.FindGameObjectWithTag("SpawnRed");
        }
        player.transform.position = spawnpoint.transform.position;
    }

    public void Victory(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Victory();
        GameObject.FindGameObjectWithTag("Victory").GetComponent<UnityEngine.UI.Text>().text = "Victory!";
        GameObject.FindGameObjectWithTag("Restart").GetComponent<UnityEngine.UI.Text>().text = "Press Q to restart";
    }

}
