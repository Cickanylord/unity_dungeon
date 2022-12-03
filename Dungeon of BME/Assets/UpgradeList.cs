using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeList : MonoBehaviour
{
    private List<UpgradeItem> list = new List<UpgradeItem>();


    // Start is called before the first frame update
    void Start()
    {
        list.Add(new UpgradeItem() { Description = "Gain +1 sword damage",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.GetComponent<SwordAttack>().damage++;
                    return 0;}, OneUse = false});
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UpgradeItem getUpgrade(){
        return list[0];
    }

    public void remove(UpgradeItem ui){
        list.Remove(ui);
    }
}
