using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerminalTextManager : MonoBehaviour
{
    [Header("Text Values")]
    public TextMeshProUGUI terminalTextUI;

    public void startWriteText(string textToWrite)
    {
        //string existingTextToOverwrite = "> " + new string('*', textToWrite.Length) + "\n\n>" + existingText;
        string currentText = terminalTextUI.text;
        terminalTextUI.text = "> " + textToWrite + "\n\n" + currentText;
    }
}
