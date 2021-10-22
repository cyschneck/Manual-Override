using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalTextManager : MonoBehaviour
{
    // Controls printing text to terminal

    [Header("Text Values")]
    public TextMeshProUGUI terminalTextUI;

    public void startWriteText(string textToWrite)
    {
        string currentText = terminalTextUI.text;
        terminalTextUI.text = "> " + textToWrite + "\n\n" + currentText;
    }
}
