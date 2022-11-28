using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    public bool isOpen;
    private Animator animator;
    private BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChest()
    {
        if(!isOpen)
        {
            isOpen = true;
            animator.SetBool("isOpen", isOpen);
            col.size = new Vector2(0.3167033f, 0.2551677f);
            col.offset = new Vector2(-0.001648307f, -0.01648343f);
        }
    }
}
