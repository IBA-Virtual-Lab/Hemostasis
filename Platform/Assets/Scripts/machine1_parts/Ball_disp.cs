using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball_disp : Interactable
{

    public MODE_TRACKER MODE_TRACKER_script;
    private Cuvette cuvettes;
    public Ball_disp preheat_hole_script;

    // Start is called before the first frame update

    public float pointerDownTimer;
    [SerializeField]
    private GameObject ball_dis;
    public bool ballsDispensed;

    //private TextMeshProUGUI textmesH;
    //public GameObject hol_gm_obj;

    private const string lable1 = "Ball Dispenser";
    void Start()
    {
        ballsDispensed = false;
        promptMessage = lable1;
        MODE_TRACKER_script = FindObjectOfType<MODE_TRACKER>();
        cuvettes = FindObjectOfType<Cuvette>();
    }
    protected override void Interact()
    {
        Debug.Log("Interacted with" + gameObject.name);
        pointerDownTimer += Time.deltaTime;
        if(pointerDownTimer > 1){
            if(cuvettes.inPosition){
                ballsDispensed = true;
                ball_dis.GetComponent<Animator>().SetTrigger("Ball_trg");
                // cuv_pos_flag = 0; (Why was this ever here lol)
            }
        }

    }
}
