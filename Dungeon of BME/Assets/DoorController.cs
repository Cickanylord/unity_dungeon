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
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] spawnpoint = GameObject.FindGameObjectsWithTag("SpawnFinal");
        if(keyTag =="GreenKey"){
            spawnpoint = GameObject.FindGameObjectsWithTag("SpawnBlue");
        }
        if(keyTag == "BlueKey"){
            spawnpoint = GameObject.FindGameObjectsWithTag("SpawnRed");
        }
        player[0].transform.position = spawnpoint[0].transform.position;
    }

    public void Victory(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Victory();
        GameObject.FindGameObjectWithTag("Victory").GetComponent<UnityEngine.UI.Text>().text = "Victory!";
        GameObject.FindGameObjectWithTag("Restart").GetComponent<UnityEngine.UI.Text>().text = "Press Q to restart";
    }

}
