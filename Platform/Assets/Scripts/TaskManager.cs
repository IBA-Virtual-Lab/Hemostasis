//// TaskManager.cs
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class TaskManager : MonoBehaviour
//{
//    public static TaskManager Instance;

//    [SerializeField] private Text warningText; // Reference to a UI Text element for displaying warnings
//    [SerializeField] private GameObject warningPanel; // Reference to the UI panel for warnings

//    private List<string> correctOrder = new List<string> {
//        "refrigerator", "combineReagents", "turnOn", "configure", "buffer",
//        "prepCuvette", "loadCuvette", "incubate", "loadPipette", "setPipette",
//        "timer", "measure", "reagentPipette", "record"
//    };


//    private void OnMouseDown()
//    {
//        // Assuming the TaskManager is a singleton
//        TaskManager.Instance.PerformTask(tasks[0]); // Assuming tasks[0] is the primary task for this GameObject
//    }


//    private List<string> playerOrder = new List<string>();

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void PerformTask(string taskName)
//    {
//        playerOrder.Add(taskName);
//        CheckOrder();
//    }

//    void CheckOrder()
//    {
//        if (!IsCorrectOrder())
//        {
//            ShowWarningMessage("Incorrect task order! Please follow the correct order.");
//            playerOrder.Clear();
//        }
//    }

//    bool IsCorrectOrder()
//    {
//        if (playerOrder.Count != correctOrder.Count)
//        {
//            return false;
//        }

//        for (int i = 0; i < correctOrder.Count; i++)
//        {
//            if (playerOrder[i] != correctOrder[i])
//            {
//                return false;
//            }
//        }

//        return true;
//    }

//    void ShowWarningMessage(string message)
//    {
//        warningText.text = message; // Set the warning text
//        warningPanel.SetActive(true); // Activate the warning panel
//    }
//}
