using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public Camera startupCamera;
    public Camera ptinrCamera;
    public Camera apttCamera;
    public Camera fibrinogenCamera;

    //void Start()
    //{
    //    // Activate the startup camera initially
    //    startupCamera.gameObject.SetActive(true);
    //    ptinrCamera.gameObject.SetActive(false);
    //    apttCamera.gameObject.SetActive(false);
    //    fibrinogenCamera.gameObject.SetActive(false);
    //}

    public void ActivatePTINR()
    {
        startupCamera.gameObject.SetActive(false);
        ptinrCamera.gameObject.SetActive(true);
        apttCamera.gameObject.SetActive(false);
        fibrinogenCamera.gameObject.SetActive(false);
    }

    public void ActivateAPTTCamera()
    {
        startupCamera.gameObject.SetActive(false);
        ptinrCamera.gameObject.SetActive(false);
        apttCamera.gameObject.SetActive(true);
        fibrinogenCamera.gameObject.SetActive(false);
    }

    public void ActivateFibrinogenCamera()
    {
        startupCamera.gameObject.SetActive(false);
        ptinrCamera.gameObject.SetActive(false);
        apttCamera.gameObject.SetActive(false);
        fibrinogenCamera.gameObject.SetActive(true);
    }
}
