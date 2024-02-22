using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public bool refreshGenPrompted = false;
    public bool pauseSceneLoaded;

    private void Update()
    {
        if (!pauseSceneLoaded)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Paused");
                Time.timeScale = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
                StartCoroutine(WaitAndPause());
            }
        }
        if (pauseSceneLoaded)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Pause");
                StartCoroutine(WaitAndUnpause());
            }
            
            
        }
    }
    IEnumerator WaitAndPause()
    {
        yield return new WaitForEndOfFrame();
        pauseSceneLoaded = true;
    }

    IEnumerator WaitAndUnpause()
    {
        yield return new WaitForEndOfFrame();
        pauseSceneLoaded = false;
    }
    public void RefreshGen()
    {
        refreshGenPrompted = true;
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }
    
}
