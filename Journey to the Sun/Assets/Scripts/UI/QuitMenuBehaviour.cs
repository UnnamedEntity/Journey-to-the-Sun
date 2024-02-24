using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitMenuBehaviour : MonoBehaviour
{
    public Button ResumeButton;
    GameObject SceneManagerObj;
    SceneManager SceneManager;

    void Start()
    {
        SceneManagerObj = GameObject.Find("SceneManager");
        SceneManager = SceneManagerObj.GetComponent<SceneManager>();

        Button btn = ResumeButton.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        Time.timeScale = 1;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("Pause"))
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Pause");
        }
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("HealthScene"))
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("HealthScene");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("WinScene");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        SceneManager.pauseSceneLoaded = false;
    }
}
