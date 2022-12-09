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
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.GetComponent<SwordAttack>().damage += 1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain +0.5 speed",
             Action = () => {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().maxSpeed += 0.5f;
                    return 0;}, OneUse = false});
        
        list.Add(new UpgradeItem() { Description = "Your maximum health increases by 2",
             Action = () => {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().MaxHealth += 2f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Your mana regenerates faster",
             Action = () => {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().manaIncremention += 0.01f ;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain +1 life steal",
             Action = () => {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().lifeSteal += 1f;
                    return 0;}, OneUse = false});
        
        list.Add(new UpgradeItem() { Description = "Your maximum mana increases by 2",
             Action = () => {GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().MaxMana += 2f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain +1 wand damage",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ShootWand>().damage += 1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain +1 arrow damage",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<ShootingController>().damage =+ 1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Your wand attack costs less mana",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ShootWand>().ManaCost -= 1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Your special wand attack costs less mana",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ShootWand>().SpecialManaCost -= 1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain more wand attack speed",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<ShootWand>().fireRate -= 0.1f;
                    return 0;}, OneUse = false});

        list.Add(new UpgradeItem() { Description = "Gain more bow attack speed",
             Action = () => {GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject.transform.GetChild(2).gameObject.GetComponent<ShootingController>().fireRate -= 0.1f;
                    return 0;}, OneUse = false});
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UpgradeItem getUpgrade(){
        System.Random rnd = new System.Random();
        int idx = rnd.Next(0, list.Count);
        return list[idx];
    }

    public void remove(UpgradeItem ui){
        list.Remove(ui);
    }
}
