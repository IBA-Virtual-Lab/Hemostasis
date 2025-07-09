using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> PTINRobjects;
    [SerializeField] private List<GameObject> APTTobjects;
    [SerializeField] private List<GameObject> Fibobjects;

    public void ActivatePTINR()
    {
        foreach (GameObject obj in PTINRobjects)
        {
            obj.SetActive(true);
        }
    }

    public void ActivateAPTT()
    {
        foreach (GameObject obj in APTTobjects)
        {
            obj.SetActive(true);
        }
    }

    public void ActivateFib()
    {
        foreach (GameObject obj in Fibobjects)
        {
            obj.SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        foreach (GameObject obj in PTINRobjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in APTTobjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in Fibobjects)
        {
            obj.SetActive(false);
        }
    }
}