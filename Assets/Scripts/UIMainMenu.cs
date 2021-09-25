using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startButtonText;
    [SerializeField] private string start;

    private void Start()
    {
        startButtonText.text = start;
    }

    public void HandleStartGameButton()
    {
        gameObject.SetActive(false);
    }
}
