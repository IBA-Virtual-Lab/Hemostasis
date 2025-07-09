using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPLR : Interactable
{
    public int rhol_selected;
    
    public float pointerDownTimer;

    private GameObject zoom_cam;
    private GameObject beed1;
    private GameObject beed2;
    private GameObject beed3;
    private GameObject beed4;
    private RHOL rhol; 
    // Start is called before the first frame update
    void Start()
    {
        beed1 = GameObject.Find("beed1");
        beed2 = GameObject.Find("beed2");
        beed3 = GameObject.Find("beed3");
        beed4 = GameObject.Find("beed4");
        zoom_cam = GameObject.Find("BeadCamera");
        rhol = FindObjectOfType<RHOL>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            beed1.SetActive(true);
            beed2.SetActive(true);
            beed3.SetActive(true);
            beed4.SetActive(true);
            zoom_cam.SetActive(true);
            rhol.beadsMoving = true;
        }
    }
}
