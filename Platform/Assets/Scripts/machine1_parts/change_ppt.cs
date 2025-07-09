using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class change_ppt : Interactable
{
    public pipt3 ppt3_script;
    // Start is called before the first frame update

    public float pointerDownTimer;

    public int GameIsPaused = 0;

    public int entery_completed = 0;

    public string inputvalue;

    public Text obj_text;
    public InputField display;

    [SerializeField]
    private GameObject inputz;
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject pipt3;


    private bool pipt3_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Pippette tips";
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
        ppt3_script = FindObjectOfType<pipt3>();
        if(ppt3_script.pipt3_selected == 1){
            promptMessage = lable2;
        }
        else{
            promptMessage = lable1;
        }
        if(entery_completed == 1){
            //Debug.Log("Inputed value: "+ entery_completed);
        }


        //Debug.Log("Status "+ entery_completed);



        if(GameIsPaused == 1){

            //Debug.Log("PPPPPPPPPPPPPPPPPPPPP");

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

            if(ppt3_script.pipt3_selected == 1){

                pipt3_use =!pipt3_use;
                Debug.Log("Do This ");
                pipt3.GetComponent<Animator>().SetTrigger("change_tipp");
                ppt3_script = FindObjectOfType<pipt3>();
                ppt3_script.pipt3_selected = 0;
                //Pause();

            }

        }

        if(entery_completed == 1){
            Debug.Log("Entery Done");
        }

    }
}
