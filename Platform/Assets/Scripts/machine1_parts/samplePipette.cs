using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SamplePipette : Interactable
{
    public bool pipt2_selected;
    public float pointerDownTimer;
    public float spaX_pos;
    private Vector3 spa_pos;

    public MODE_TRACKER MODE_TRACKER_script;
    public int capsule_pos;
    public int GameIsPaused = 0;

    [SerializeField]
    public Material default_material;
    [SerializeField]
    private Material selected_material;
    [SerializeField]
    private GameObject pipet;
    [SerializeField]
    public GameObject pipet_skin;
    [SerializeField]
    private GameObject spa;
    [SerializeField]
    private GameObject pauseMenuUI;

    public int pipetteCount; // The amount of times it has been used to pipette

    // Start is called before the first frame update
    void Start()
    {
        pipt2_selected = false;
        pipetteCount = 0;
        pauseMenuUI.SetActive(false);
        capsule_pos = 0;
        MODE_TRACKER_script = FindObjectOfType<MODE_TRACKER>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameIsPaused == 1){   
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Resume();
            }
        }

/*
    Why make it this complicated
        if(Input.GetKeyDown(KeyCode.F))
        {
            capsule_pos = 0;
        }
        if(Input.GetKeyDown(KeyCode.B)){
            capsule_pos = 1;
        }
        if(Input.GetKeyDown(KeyCode.R)){
            capsule_pos = 2;
        }
        if(Input.GetKeyDown(KeyCode.L)){
            capsule_pos = 3;
        }
        */
    }

//TODO: Remove the separate pause/resume functions. There are separate functions for this in multiple scripts..
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
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            pipt2_selected = !pipt2_selected;
            if (pipt2_selected){
                pipet_skin.GetComponent<MeshRenderer>().material = selected_material;
                //pipet.GetComponent<Animator>().SetTrigger("tgr1_ppt1");
            } else{
                pipet_skin.GetComponent<MeshRenderer>().material = default_material;
            }
            pointerDownTimer = 0;
        }
    }
}
