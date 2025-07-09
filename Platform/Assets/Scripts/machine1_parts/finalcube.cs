using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class finalcube : Interactable
{

    private pipt ppt_script;
    private float pointerDownTimer;
    private float spaX_pos;
    private Vector3 spa_pos;
    private int spaLoc_flag;
    private float cvtspaX_pos;
    private Vector3 cvtspa_pos;
    private int cvtspaLoc_flag;
    private string inputvalue;
    private Text obj_text;
    private InputField display;

    public int ppt1_in_place;

    private bool inputActive = false;

    [SerializeField]
    private GameObject inputz;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject pipt;
    [SerializeField]
    private GameObject cvt;
    private bool pipt_use;

    private const string label1 = " ";
    private const string label2 = "Place Solution";
    private const string label3 = "Set Preheat";

    void Start()
    {
        ppt_script = FindObjectOfType<pipt>();
        promptMessage = label1;
        inputvalue = "Nope";
    }

    // Update is called once per frame
    void Update()
    {
        spa_pos = pipt.transform.position;
        spaX_pos = pipt.transform.position.x;
        //Debug.Log("SAP Loc" + spaX_pos);
        if (spaX_pos == 0.0846f){
            spaLoc_flag=1;
            ppt1_in_place =1;
            //Debug.Log("INPOSSSS");
        }
        cvtspa_pos = cvt.transform.position;
        cvtspaX_pos = cvt.transform.position.x;
        //Debug.Log("SAP Loc" + spaX_pos);
        if (cvtspaX_pos == 0.0284f){
            cvtspaLoc_flag=1;
            //Debug.Log("CVT in position");
        }

        if(ppt_script.pipt_selected == 1){
            promptMessage = label2;
        }
        else{
            promptMessage = label1;
        }

        if(inputActive == true){
            if(Input.GetKeyDown(KeyCode.Return))
            {
                inputvalue = inputz.GetComponent<InputField>().text;
                Debug.Log(inputvalue);
            }
        }        
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(ppt_script.pipt_selected == 1){
                pipt_use =!pipt_use;
                if(cvtspaLoc_flag == 1 && spaLoc_flag ==1  )
                {
                    pipt.GetComponent<Animator>().SetTrigger("final tgr");
                    ppt_script = FindObjectOfType<pipt>();
                    ppt_script.pipt_selected = 0;
                }
            }

        }
    }
}