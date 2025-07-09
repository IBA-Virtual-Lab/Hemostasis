using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RHOL : Interactable
{
    public Cuvette cuvettes;

    public CPLR script2;


    [SerializeField]
    private GameObject zoom_cam;

    [SerializeField]
    private GameObject beed1;

    [SerializeField]
    private GameObject beed2;

    [SerializeField]
    private GameObject beed3;

    [SerializeField]
    private GameObject beed4;

    public float pointerDownTimer;

    [SerializeField]
    private GameObject cuvette;
    private bool testtube_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Measurement Channel";
    private const string lable2 = "Place Test Tube in Measurement Channel";

    public bool beadsMoving;

    private float beadTimer = 0.0f;
    private float beadTime = 60.0f;

    private pipt reagentPipette;

    // Start is called before the first frame update
    void Start()
    {
        //textmesH = hol_gm_obj.GetComponent<TextMeshProUGUI>();
        promptMessage = lable1;
        zoom_cam.SetActive(false);
        beed1.SetActive(false);
        beed2.SetActive(false);
        beed3.SetActive(false);
        beed4.SetActive(false);
        cuvettes = FindObjectOfType<Cuvette>();
        script2 = FindObjectOfType<CPLR>();
        beadsMoving = false;
        reagentPipette = FindObjectOfType<pipt>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
            wtf is this

        // If cuvette in position and rhol button pressed, beads start moving
        if (script2.rhol_selected == 1 && cuvette.transform.position.x == 0.0284f){
            Debug.Log("Show beed");
            beed1.SetActive(true);
            beed2.SetActive(true);
            beed3.SetActive(true);
            beed4.SetActive(true);
            zoom_cam.SetActive(true);
            beadsMoving = true;
        }
        //Debug.Log("ANOTHER" + script.testube_selected);
        if(cuvettes.cuvette_selected){
            promptMessage = lable2;
        }
        else{
            promptMessage = lable1;
        }

        //Debug.Log("FINAL LOC" + testtube.transform.position.x);

        if(cuvette.transform.position.x == 0.0284f){
            Debug.Log("Show beed");
            beed1.SetActive(true);
            beed2.SetActive(true);
            beed3.SetActive(true);
            beed4.SetActive(true);
            //zoom_cam.SetActive(true);
            beadsMoving = true;
        }
        */
        if(beadsMoving && reagentPipette.finished){ // bool check in update is not cpu intensive
            beadTimer += Time.deltaTime;
            if (beadTimer >= beadTime) {
                // Stop the beads from moving
                beadsMoving = false;
                // Reset the timer
                beadTimer = 0.0f;
            }
            // Access the beads animation component and reduce their animation speed
            beed1.GetComponent<Animator>().speed = 1 - (beadTimer / beadTime);
            beed2.GetComponent<Animator>().speed = 1 - (beadTimer / beadTime);
            beed3.GetComponent<Animator>().speed = 1 - (beadTimer / beadTime);
            beed4.GetComponent<Animator>().speed = 1 - (beadTimer / beadTime);
        }else if(!beadsMoving && reagentPipette.finished){
            beed1.GetComponent<Animator>().speed = 0;
            beed2.GetComponent<Animator>().speed = 0;
            beed3.GetComponent<Animator>().speed = 0;
            beed4.GetComponent<Animator>().speed = 0;
        }
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            testtube_use =!testtube_use;
            cuvette.GetComponent<Animator>().SetTrigger("final_pos");
            cuvettes = FindObjectOfType<Cuvette>();
            cuvettes.cuvette_selected = false;
        }
    }
}
