using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mix : Interactable
{
    public pipt ppt_script;
    public SPA_Preheat preheat_hole_script;
    // Start is called before the first frame update

    public float pointerDownTimer;

    [SerializeField]
    private GameObject pipt;


    [SerializeField]
    private GameObject SPA_Preheat;


    private bool pipt_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Patient Plasma";
    private const string lable2 = "Take Plasma";
    private const string lable3 = "Set Preheat";


    void Start()
    {
        promptMessage = lable1;
        ppt_script = FindObjectOfType<pipt>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ANOTHER" + script.testube_selected);
        if(ppt_script.pipt_selected == 1){
            Debug.Log("ANOTHER");
            promptMessage = lable2;

        }

        preheat_hole_script = FindObjectOfType<SPA_Preheat>();
                //Debug.Log("ANOTHER" + script.testube_selected);
  /*      if(preheat_hole_script.preheat_selected){

            Debug.Log("ANOTHER");
            promptMessage = lable3;

        }
*/
        promptMessage = lable1;
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);

        pointerDownTimer += Time.deltaTime;

        if(pointerDownTimer > 1){

            if(ppt_script.pipt_selected == 1){

                pipt_use =!pipt_use;
                pipt.GetComponent<Animator>().SetTrigger("trigger_pip");
                ppt_script = FindObjectOfType<pipt>();
                ppt_script.pipt_selected = 0;

            }
  /*          
            if(preheat_hole_script.preheat_selected){

                pipt_use =!pipt_use;
                SPA_Preheat.GetComponent<Animator>().SetTrigger("preheat_on");
                preheat_hole_script = FindObjectOfType<SPA_Preheat>();
                preheat_hole_script.preheat_selected = false;

            }
*/


        }

    }
}
