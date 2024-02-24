using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthTextBehaviour : MonoBehaviour
{
    GameObject _Player;
    PlayerBehaviour _PlayerBehaviour;

    TMP_Text _Text;

    private void Start()
    {
        _Player = GameObject.Find("Player");
        _PlayerBehaviour = _Player.GetComponent<PlayerBehaviour>();
        _Text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        _Text.text = _PlayerBehaviour.Health.ToString();
    }
}
