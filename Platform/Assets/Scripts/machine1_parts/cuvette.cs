using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cuvette : Interactable
{

    public bool cuvette_selected;
    public float pointerDownTimer;

    private bool pipt3_use;

    [SerializeField]
    private Material default_material;
    [SerializeField]
    private Material selected_material;
    [SerializeField]
    private GameObject Gm_obj;
    private float xPos;
    public bool inPosition;
    public bool inFinalPosition;

    private pipt reagentPipette;
    private GameObject reagentPipetteAnimationObject;
    void Start()
    {
        cuvette_selected = false;
        reagentPipette = FindObjectOfType<pipt>();
        reagentPipetteAnimationObject = GameObject.Find("pip_both");
    }

    void Update()
    {
        if(!cuvette_selected){
            Gm_obj.GetComponent<MeshRenderer>().material = default_material;
        }
        xPos = transform.position.x;
        if(xPos == -0.0908f)
        {
            inPosition = true;
        }else{
            inPosition = false;
        }
        if(xPos == 0.0284f){
            inFinalPosition = true;
        }else{
            inFinalPosition = false;
        }
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            cuvette_selected = !cuvette_selected;

            if (cuvette_selected){
                Gm_obj.GetComponent<MeshRenderer>().material = selected_material;
                if(reagentPipette.filled){
                    Gm_obj.GetComponent<MeshRenderer>().material = default_material;
                    reagentPipetteAnimationObject.GetComponent<Animator>().SetTrigger("deposit_reagent");
                    reagentPipette.filled = false;
                    reagentPipette.finished = true;
                }
            }
            else{
                Gm_obj.GetComponent<MeshRenderer>().material = default_material;
            }
            pointerDownTimer = 0;
        }
    }
}
