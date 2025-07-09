using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pipt3 : Interactable
{

    
    public int pipt3_selected;
    public float pointerDownTimer;
    public float spaX_pos;
    private Vector3 spa_pos;


    //Vector3 target_position = new Vector3(transform.position.x,target.position.y,transform.position.z);



    public int capsule_pos;

    public int GameIsPaused = 0;

    public int spaLoc_flag;

    [SerializeField]
    private Material default_material;
    [SerializeField]
    private Material selected_material;


    [SerializeField]
    private GameObject pipet;

    [SerializeField]
    private GameObject pipet_skin;


    [SerializeField]
    private GameObject spa;

    [SerializeField]
    private GameObject pauseMenuUI;



    // Start is called before the first frame update
    void Start()
    {
        pipt3_selected = 0;
        spaLoc_flag=0;
        pauseMenuUI.SetActive(false);
        capsule_pos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spa_pos = spa.transform.position;
        spaX_pos = spa.transform.position.x;
        //Debug.Log("SAP Loc" + spaX_pos);
        if (spaX_pos == 0.145f){
            spaLoc_flag=1;
        }

        //Debug.Log("PPPPPPPPPPPPPPPPPPPPP"+GameIsPaused);

        if(GameIsPaused == 1){
            
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Resume();
            }
        }


        if(Input.GetKeyDown(KeyCode.F))
        {
            capsule_pos = 0;
        }
        if(Input.GetKeyDown(KeyCode.B)){

            //loccap.transform.position = new Vector3(0f, 0f, 13f);

            //Debug.Log("Cap lox" + loccap.transform.position);

            //MoveGameObject();

            capsule_pos = 1;
        }
        if(Input.GetKeyDown(KeyCode.R)){
            capsule_pos = 2;
        }
        if(Input.GetKeyDown(KeyCode.L)){
            capsule_pos = 3;
        }
    


        
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
         GameIsPaused = 0;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = 1;

    }

    protected override void Interact()
    {
        //Debug.Log("Interacted with" + gameObject.name);

        //pipet.GetComponent<Animator>().SetTrigger("trigger_pip");

        pointerDownTimer += Time.deltaTime;

        if(pointerDownTimer > 1){

            pipt3_selected = pipt3_selected + 1;
            if (pipt3_selected > 1){
                pipt3_selected = 0;
            }

            if (pipt3_selected == 1){
                //Debug.Log("TestTube seleled");
                pipet_skin.GetComponent<MeshRenderer>().material = selected_material;
                if(spaLoc_flag ==1){
                    Debug.Log("EXE_ACT");
                    pipet.GetComponent<Animator>().SetTrigger("trigger_pip");
                }

                //Pause();

            }
            else{
                //Debug.Log("TestTube Unseleled");
                pipet_skin.GetComponent<MeshRenderer>().material = default_material;
            }
            pointerDownTimer = 0;

        }

        

    }
}
