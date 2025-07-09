using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PatientPlasma : Interactable
{
    public MODE_TRACKER MODE_TRACKER_script;
    public SamplePipette samplePipette;
    public float pointerDownTimer;
    public int GameIsPaused = 0;
    public bool entryCompleted;
    public string inputvalue;
    public Text obj_text;
    public InputField display;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject pipt2;
    private bool pipt2_use;
    private const string label1 = "Patient Plasma";
    private const string label2 = "Take Patient Plasma";

    void Start()
    {
        promptMessage = label1;
        inputvalue = "";
        MODE_TRACKER_script = FindObjectOfType<MODE_TRACKER>();
        samplePipette = FindObjectOfType<SamplePipette>();   
        entryCompleted = false;     
    }

    void Update()
    {
        if(samplePipette.pipt2_selected){
            promptMessage = label2;
        }
        else{
            promptMessage = label1;
        }

        //Debug.Log("Status "+ entery_completed);
        if(GameIsPaused == 1){
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Resume();
                //inputvalue = inputz.GetComponent<InputField>().text; - Broken
                Debug.Log(inputvalue);
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        entryCompleted = true;
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
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(samplePipette.pipt2_selected){
                pipt2_use = !pipt2_use;
                string animationToPlay = samplePipette.pipetteCount switch{ // Triggers animation when pipette is selected and patient sample is interacted with
                    0 => "tgr1_ppt1",
                    1 => "tgr1_ppt2",
                    2 => "tgr1_ppt3",
                    3 => "tgr1_ppt4",
                    4 => "tgr1_ppt2",
                    8 => "tgr1_ppt3",
                    12 => "tgr1_ppt4",
                    _ => "tgr1_ppt1"
                };
                pipt2.GetComponent<Animator>().SetTrigger(animationToPlay);
                samplePipette = FindObjectOfType<SamplePipette>();
                samplePipette.pipt2_selected = false;
                samplePipette.pipet_skin.GetComponent<MeshRenderer>().material = samplePipette.default_material;
                samplePipette.pipetteCount += 4;
                Debug.Log(samplePipette.pipetteCount);
                Pause(); // Pauses for entry of ml to pipette
            }
        }
    }
}
