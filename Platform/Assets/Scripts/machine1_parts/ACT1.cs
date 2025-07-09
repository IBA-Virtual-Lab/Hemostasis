using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ACT1 : Interactable
{
    [SerializeField]
    private GameObject testtube;
    private bool testtube_use;
    public int act1;
    private Animator testtube_animator;
    // Start is called before the first frame update
    void Start()
    {
        act1 = 0;
        testtube_animator = testtube.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("act1 is " + act1);
    }

    protected override void Interact()
    {
        act1 = 1;
        Debug.Log("Interacted with" + gameObject.name);
        testtube_use =!testtube_use;
        testtube_animator.SetBool("isTest",testtube_use);
    }
}
