using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Easy()
    {
        SceneManager.LoadScene(2);
    }

    public void Hard()
    {
        SceneManager.LoadScene(3);
    }
}