using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Objects")]
    public GameObject mainMenuUI;
    public GameObject creditsUI;

    public void StartGame()
    {
        SceneManager.LoadScene("Terminal");
    }

    public void DisplayCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        mainMenuUI.SetActive(true);
        creditsUI.SetActive(false);
    }
}
