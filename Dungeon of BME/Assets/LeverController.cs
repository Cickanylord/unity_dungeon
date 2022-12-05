using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LeverController : MonoBehaviour
{

    public bool isOpen;
    private Animator animator;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDoor()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpen", isOpen);
        door.GetComponent<Animator>().SetBool("triggerOpen", isOpen);
        door.GetComponent<BoxCollider2D>().isTrigger = isOpen;
        if(isOpen && door.tag == "Door1")
        {
            door.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        else {
            door.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
