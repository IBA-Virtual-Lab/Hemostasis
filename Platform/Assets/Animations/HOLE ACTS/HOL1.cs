using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HOL1 : Interactable
{
    public Cuvette cuvettes;


    [SerializeField]
    private GameObject zoom_cam;

    public float pointerDownTimer;

    [SerializeField]
    private GameObject testtube;
    private bool testtube_use;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Well 1";
    private const string lable2 = "Place Test Tube in Well 1";


    // Start is called before the first frame update
    void Start()
    {
        //textmesH = hol_gm_obj.GetComponent<TextMeshProUGUI>();
        promptMessage = lable1;
        zoom_cam.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        cuvettes = FindObjectOfType<Cuvette>();
        if(cuvettes.cuvette_selected){
            promptMessage = lable2;
        }
        else{
            promptMessage = lable1;
        }
    }
    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;

        if(pointerDownTimer > 1){
            testtube_use =!testtube_use;
            //zoom_cam.SetActive(true);
            testtube.GetComponent<Animator>().SetTrigger("testtb1");
            //testtube.GetComponent<Animator>().SetBool("isTest",testtube_use);
            cuvettes = FindObjectOfType<Cuvette>();
            cuvettes.cuvette_selected = false;
        }
    }
}
