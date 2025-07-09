using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pipt : Interactable
{   
    public MODE_TRACKER MODE_TRACKER_script;
    public int pipt_selected;
    public float pointerDownTimer;
    public int bottle_in_place;
    public int spaLoc_flag;
    public bool reagentReady;
    public int GameIsPaused = 0;
    public int entery_completed = 0;
    public int pause_count = 0;
    public string inputvalue;
    public Text obj_text;
    public InputField display;

    public bool filled;
    public bool finished;

    [SerializeField]
    private GameObject inputz;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI2;
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

    void Start()
    {
        MODE_TRACKER_script = FindObjectOfType<MODE_TRACKER>();
        pipt_selected = 0;
        spaLoc_flag=0;
        pause_count = 0;
        pauseMenuUI.SetActive(false);
        pauseMenuUI2.SetActive(false);
        finished = false;
        filled = false;
        reagentReady = false;
    }

    void Update()
    {
        if(GameIsPaused == 1){
            if(Input.GetKeyDown(KeyCode.Return))
            {
                pause_count += 1;
                if (pause_count == 1){
                    pauseMenuUI.SetActive(false);
                    pauseMenuUI2.SetActive(true);
                }
                if(pause_count > 1){
                    Resume();
                }
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuUI2.SetActive(false);
        entery_completed = 1;
        Time.timeScale = 1f;
        GameIsPaused = 0;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = 1;
    }

    void Pause2(){
        pauseMenuUI2.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = 1;
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            pipt_selected = pipt_selected + 1;
            if (pipt_selected > 1){
                pipt_selected = 0;
            }
            if (pipt_selected == 1){
                pipet_skin.GetComponent<MeshRenderer>().material = selected_material;
                if(reagentReady){
                    pipet.GetComponent<Animator>().SetTrigger("trigger_pip");
                    filled = true;
                }
                Pause();
            }
            else{
                pipet_skin.GetComponent<MeshRenderer>().material = default_material;
                pipt_selected = 0;
            }
            pointerDownTimer = 0;
        }
    }
}
