using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shake : Interactable
{
    public pipt ppt_script;
    // Start is called before the first frame update

    public float pointerDownTimer;

    public int GameIsPaused = 0;

    public int entery_completed = 0;

    public float spaX_pos;
    private Vector3 spa_pos;

    public int spaLoc_flag;


    public float cvtspaX_pos;
    private Vector3 cvtspa_pos;

    public int cvtspaLoc_flag;

    public string inputvalue;

    public Text obj_text;
    public InputField display;



    [SerializeField]
    private GameObject inputz;
    

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject pipt;

    [SerializeField]
    private GameObject cvt;


    private bool pipt_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Place Solution";
    private const string lable2 = "Change Pippette tip";
    private const string lable3 = "Set Preheat";


    void Start()
    {
        promptMessage = lable1;
        inputvalue = "Nope";
        
    }

    // Update is called once per frame
    void Update()
    {

        spa_pos = pipt.transform.position;
        spaX_pos = pipt.transform.position.x;
        Debug.Log("SAP Loc" + spaX_pos);
        if (spaX_pos == 0.0846f){
            spaLoc_flag=1;
            Debug.Log("INPOSSSS");

        }


        cvtspa_pos = cvt.transform.position;
        cvtspaX_pos = cvt.transform.position.x;
        Debug.Log("SAP Loc" + spaX_pos);
        if (cvtspaX_pos == 0.0284f){
            cvtspaLoc_flag=1;
            Debug.Log("CVTTTT INPOSSSS");

        }

        ppt_script = FindObjectOfType<pipt>();
        //Debug.Log("ANOTHER" + script.testube_selected);
        if(ppt_script.pipt_selected == 1){

            Debug.Log("ANOTHER");
            promptMessage = lable2;

        }
        else{
            promptMessage = lable1;
        }

        if(entery_completed == 1){

            Debug.Log("Inputed value: "+ entery_completed);

        }


        Debug.Log("Status "+ entery_completed);



        if(GameIsPaused == 1){

            Debug.Log("PPPPPPPPPPPPPPPPPPPPP");

            if(Input.GetKeyDown(KeyCode.Return))
            {
                Resume();
                //inputvalue = inputz.GetComponent<InputField>().text;
                //Debug.Log(inputvalue);
            }
        }
        
    }



    void Resume()
    {
        pauseMenuUI.SetActive(false);
        entery_completed = 1;
        Time.timeScale = 1f;
        GameIsPaused = 0;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = 1;

    }

    public void ReadStringInput(string s)
    {
        inputvalue = s;
    }


    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);

        pointerDownTimer += Time.deltaTime;

        if(pointerDownTimer > 1){

            if(ppt_script.pipt_selected == 1){

                pipt_use =!pipt_use;
                if(cvtspaLoc_flag == 1 && spaLoc_flag ==1  )
                {

                    Debug.Log("FINAL ACTION ");
                    pipt.GetComponent<Animator>().SetTrigger("final tgr");
                    ppt_script = FindObjectOfType<pipt>();
                    ppt_script.pipt_selected = 0;

                }
                //Debug.Log("Do This pp2 ");

                //Pause();

            }

        }

        if(entery_completed == 1){
            Debug.Log("Entery Done");
        }

    }
}