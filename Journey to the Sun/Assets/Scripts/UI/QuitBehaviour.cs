using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitBehaviour : MonoBehaviour
{
    public Button StartButton;
    void Start()
    {
        Button btn = StartButton.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);
    }
  
    void ButtonClicked()
    {
        Application.Quit();
    }
}
