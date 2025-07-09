using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Machine11; // Name of the scene to load

    public void LoadScene()
    {
        SceneManager.LoadScene(Machine11);
    }
}
