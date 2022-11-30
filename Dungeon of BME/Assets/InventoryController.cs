using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour{

    public int selectedWeapon = 0;
    public int weaponCount = 3;
    public GameObject greenKey;
    public GameObject blueKey;
    public GameObject redKey;
    public GameObject finalKey;

    // Start is called before the first frame update
    void Start(){
        SelectedWeapon();
    }

    private void SelectedWeapon(){
        int i = 0;

        foreach( Transform weapon in transform){
            if(weapon.CompareTag("weapon")){
                if(i == selectedWeapon){
                    weapon.gameObject.SetActive(true);
                }
                else{
                    weapon.gameObject.SetActive(false);
                }
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update(){
        int tmp = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            if(selectedWeapon >= weaponCount-1){
               selectedWeapon = 0; 
            }
            else{
                selectedWeapon++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selectedWeapon <= 0){
                selectedWeapon = weaponCount - 1;
            }
            else{
                selectedWeapon--;
            }
        }

        if(tmp != selectedWeapon){
            SelectedWeapon();
        }

    }

    public void addGreenKey(){
        GameObject clone = Instantiate(greenKey, this.transform);
        clone.transform.parent = this.transform;
    }

    public void addBlueKey(){
        GameObject clone = Instantiate(blueKey, this.transform);
        clone.transform.parent = this.transform;
    }

    public void addRedKey(){
        GameObject clone = Instantiate(redKey, this.transform);
        clone.transform.parent = this.transform;
    }

    public void addFinalKey(){
        GameObject clone = Instantiate(finalKey, this.transform);
        clone.transform.parent = this.transform;
    }
}
