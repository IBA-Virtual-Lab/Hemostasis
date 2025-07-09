using System.Collections.Generic;
using UnityEngine;

public class InitialObjectState : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectState
    {
        public GameObject gameObject;
        public bool activeState;
    }

    [SerializeField] private List<ObjectState> objectsToManage;

    void Start()
    {
        Debug.Log("Start called!");
        foreach (var objectState in objectsToManage)
        {
            if (objectState.gameObject != null)
            {
                objectState.gameObject.SetActive(objectState.activeState);
            }
            else
            {
                Debug.LogWarning("Missing GameObject reference in InitialObjectState.");
            }
        }
    }
}
