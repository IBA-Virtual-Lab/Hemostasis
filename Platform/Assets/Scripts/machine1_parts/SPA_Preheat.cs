using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPA_Preheat : Interactable
{
    public float pointerDownTimer;
    public bool preheat_selected;

    [SerializeField]
    private Material default_material;
    [SerializeField]
    private Material selected_material;
    private GameObject SPA_Preheat_Hole;

    // Start is called before the first frame update
    void Start()
    {
        preheat_selected = false;
        SPA_Preheat_Hole = GameObject.Find("preheat_hole_1");
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            preheat_selected = !preheat_selected;
            if (preheat_selected){
                promptMessage = "Select the SPA";
                SPA_Preheat_Hole.GetComponent<MeshRenderer>().material = selected_material;
            }   
            else{
                Debug.Log("Select this pre heat well");
                SPA_Preheat_Hole.GetComponent<MeshRenderer>().material = default_material;
            }
            pointerDownTimer = 0;
        }
    }
}
