using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pat2 : Interactable
{
    public SamplePipette ppt2_script;
    public SPA_Preheat preheat_hole_script;
    public Bottle2 bottle2_script;

    public int Owren_buffer_use;
    public int pipt2_uses;
    public float pointerDownTimer;
    [SerializeField]
    private GameObject pipt2;
    [SerializeField]
    private GameObject NBX;
    [SerializeField]
    private GameObject SPA_Preheat;

    private bool pipt2_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Dilution Testtube";
    private const string lable2 = "Place Owen Buffer";
    private const string lable3 = "place Plasma";


    void Start()
    {
        promptMessage = lable1;
        Owren_buffer_use = 0;
        pipt2_uses = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ppt2_script = FindObjectOfType<SamplePipette>();
        //Debug.Log("ANOTHER" + script.testube_selected);
        if(ppt2_script.pipt2_selected){

            //Debug.Log("ANOTHER");
            promptMessage = lable2;
            pipt2_uses = 1;

        }

        bottle2_script = FindObjectOfType<Bottle2>();

        if(bottle2_script.entery_completed == 1){
            Debug.Log("ANOTHER");
            Owren_buffer_use = 1;
            promptMessage = lable2;
        }

        else{
            promptMessage = lable1;
        }

        //Debug.Log("Ob selected: " + Owren_buffer_use );
        //Debug.Log("pipet2 selected : " + Owren_buffer_use );
    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with" + gameObject.name);
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(pipt2_uses == 1 && Owren_buffer_use == 1){
                pipt2_use =!pipt2_use;
                pipt2.GetComponent<Animator>().SetTrigger("ob_put_trigger");
                //Debug.Log("Put OBpp");
                ppt2_script = FindObjectOfType<SamplePipette>();
                ppt2_script.pipt2_selected = false;
            }
            else{
                //Debug.Log("SHAKE");
                //NBX.GetComponent<Animator>().SetTrigger("shake");
            }
        }
    }
}
