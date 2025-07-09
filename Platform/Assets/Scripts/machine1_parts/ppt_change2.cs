using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ppt_change2 : Interactable
{
    public SamplePipette samplePipette;
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
    private GameObject pipt2;


    private bool pipt2_use;

    private const string label1 = "Pippette tips";
    private const string label2 = "Change Pippette tip";
    void Start()
    {
        promptMessage = label1;
        inputvalue = "Nope";
        samplePipette = FindObjectOfType<SamplePipette>();
    }

    // Update is called once per frame
    void Update()
    {
        if(samplePipette.pipt2_selected){
            promptMessage = label2;
        }
        else{
            promptMessage = label1;
        }

        if(GameIsPaused == 1){
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Resume();
            }
        }
    }

    public void ReadStringInput(string s)
    {
        inputvalue = s;
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(samplePipette.pipt2_selected){
                pipt2_use =!pipt2_use;
                pipt2.GetComponent<Animator>().SetTrigger("change_2");
                samplePipette = FindObjectOfType<SamplePipette>();
                samplePipette.pipt2_selected = false;
                samplePipette.pipet_skin.GetComponent<MeshRenderer>().material = samplePipette.default_material;
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
}