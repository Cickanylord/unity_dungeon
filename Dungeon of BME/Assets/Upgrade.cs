using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    GameObject gameController;
    public GameObject frame;
    public GameObject text;
    public bool active = false;

    private GameObject upgradeList;

    private GameObject frame1;
    private GameObject frame2;
    private GameObject frame3;

    private GameObject text1;
    private GameObject text2;
    private GameObject text3;

    UpgradeItem ui1;
    UpgradeItem ui2;
    UpgradeItem ui3;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        upgradeList = GameObject.FindGameObjectWithTag("UpgradeList");
    }


    void showUpgradeChoices(){
        gameController.GetComponent<GameController>().pause = true;
        active = true;
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        frame1 = Instantiate(frame, new Vector2(-315, 0), Quaternion.Euler(0,0,0));
        frame1.transform.SetParent(canvas.transform, false);
        frame2 = Instantiate(frame, new Vector2(0, 0), Quaternion.Euler(0,0,0));
        frame2.transform.SetParent(canvas.transform, false);
        frame3 = Instantiate(frame, new Vector2(315, 0), Quaternion.Euler(0,0,0));
        frame3.transform.SetParent(canvas.transform, false);


        text1 = Instantiate(text, new Vector2(-315, 0), Quaternion.Euler(0,0,0));
        text1.transform.SetParent(canvas.transform, false);
        text2 = Instantiate(text, new Vector2(0, 0), Quaternion.Euler(0,0,0));
        text2.transform.SetParent(canvas.transform, false);
        text3 = Instantiate(text, new Vector2(315, 0), Quaternion.Euler(0,0,0));
        text3.transform.SetParent(canvas.transform, false);

        
        text2.GetComponent<UnityEngine.UI.Text>().text = "[2]";
        text3.GetComponent<UnityEngine.UI.Text>().text = "[3]";

        ui1 = upgradeList.GetComponent<UpgradeList>().getUpgrade();
        text1.GetComponent<UnityEngine.UI.Text>().text = $"{ui1.Description} [1]";

    }

    void hideUpgradeChoices(){
        Destroy(frame1);
        Destroy(frame2);
        Destroy(frame3);

        Destroy(text1);
        Destroy(text2);
        Destroy(text3);
    }

    void Update(){
        if (active){
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                ui1.Action();
                if(ui1.OneUse){
                    upgradeList.GetComponent<UpgradeList>().remove(ui1);
                }
                hideUpgradeChoices();
                gameController.GetComponent<GameController>().pause = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)){
                ui2.Action();
                if(ui2.OneUse){
                    upgradeList.GetComponent<UpgradeList>().remove(ui2);
                }
                hideUpgradeChoices();
                gameController.GetComponent<GameController>().pause = false;
            }
            if (Input.GetKey(KeyCode.Alpha3)){
               ui3.Action();
                if(ui3.OneUse){
                    upgradeList.GetComponent<UpgradeList>().remove(ui3);
                }
                hideUpgradeChoices();
                gameController.GetComponent<GameController>().pause = false;
            }
        }

    }
}
