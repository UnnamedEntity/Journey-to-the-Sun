using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitBehaviour : MonoBehaviour
{
    public Button QuitButton;
    void Start()
    {
        Button btn = QuitButton.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);
    }
  
    void ButtonClicked()
    {
        Application.Quit();
    }
}
