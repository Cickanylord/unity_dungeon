using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour{

    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start(){
        SelectedWeapon();
    }

    private void SelectedWeapon(){
        int i = 0;

        foreach( Transform weapon in transform){
            if(i == selectedWeapon){
                weapon.gameObject.SetActive(true);
            }
            else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update(){
        int tmp = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            if(selectedWeapon >= transform.childCount -1){
               selectedWeapon = 0; 
            }
            else{
                selectedWeapon++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            if(selectedWeapon <= 0){
                selectedWeapon = transform.childCount - 1;
            }
            else{
                selectedWeapon--;
            }
        }

        if(tmp != selectedWeapon){
            SelectedWeapon();
        }

    }
}
