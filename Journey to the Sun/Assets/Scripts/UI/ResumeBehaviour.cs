using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Resume : MonoBehaviour
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
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Pause");
        SceneManager.pauseSceneLoaded = false;
    }
}
