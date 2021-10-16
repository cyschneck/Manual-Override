﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("Menus")]
    public bool referenceOpened = false;
    private GameManager gameManager;
    public GameObject pauseMenuUI;
    public GameObject referenceMenuUI;
    public GameObject referenceMenuImagesUI;
    public GameObject missionLogTextUI;
    public GameObject terminalText;
    public GameObject missionLogOutline;
    public ScrollRect contentDisplay;

    [Header("Mission Log Unlocked")]
    public bool isPlantUnlocked;
    public bool isCMEUnlocked;
    public bool isDwarfPlanetUnlocked;
    public bool isCryovolcanismUnlocked;
    public bool isGravityAssitUnlocked;
    public bool isAirCompUnlocked;
    public bool isMiningUnlocked;
    public bool isMarsUnlocked;
    public bool isAsteroidBeltUnlocked;
    public bool isJupiterUnlocked;
    public bool isJupiterMoonsUnlocked;
    public bool isSaturnUnlocked;
    public bool isTitanUnlocked;
    private string plantName;
    private string cmeName;
    private string dwarfPlanetName;
    private string cryovolcanismName;
    private string gravityAsistName;
    private string airCompName;
    private string miningName;
    private string marsName;
    private string asteroidBeltName;
    private string jupiterName;
    private string jupiterMoonsName;
    private string saturnName;
    private string titanName;

    [Header("Mission Log Buttons")]
    public GameObject plantButton;
    public GameObject cmeButton;
    public GameObject dwarfPlanetButton;
    public GameObject cryovolcansimButton;
    public GameObject gravityAssitButton;
    public GameObject miningButton;
    public GameObject airCompButton;
    public GameObject marsButton;
    public GameObject asteroidBeltButton;
    public GameObject jupiterButton;
    public GameObject jupiterMoonsButton;
    public GameObject saturnButton;
    public GameObject titanButton;
    public GameObject returnToMenuButton;

    [Header("Mission Log Text Location")]
    public GameObject plantText;
    public GameObject cmeText;
    public GameObject dwarfPlanetText;
    public GameObject cryovolcanismText;
    public GameObject gravityAssitText;
    public GameObject miningText;
    public GameObject airCompText;
    public GameObject marsText;
    public GameObject asteroidBeltText;
    public GameObject jupiterText;
    public GameObject jupiterMoonsText;
    public GameObject saturnText;
    public GameObject titanText;
    public GameObject scrollRectContainer;

    [Header("Mission Log Text")]
    public TextToDisplayMissionLog plantMissionText;
    public TextToDisplayMissionLog cmeMissionText;
    public TextToDisplayMissionLog dwarfPlanetMissionText;
    public TextToDisplayMissionLog cryovolcanismMissionText;
    public TextToDisplayMissionLog gravityAssitMissionText;
    public TextToDisplayMissionLog airCompMissionText;
    public TextToDisplayMissionLog miningMissionText;
    public TextToDisplayMissionLog marsMissionText;
    public TextToDisplayMissionLog asteroidBeltMissionText;
    public TextToDisplayMissionLog jupiterMissionText;
    public TextToDisplayMissionLog jupiterMoonsMissionText;
    public TextToDisplayMissionLog saturnMissionText;
    public TextToDisplayMissionLog titanMissionText;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // mission log reference items
        plantName = "Plants";
        cmeName = "CMEs";
        dwarfPlanetName = "Dwarf Planets";
        cryovolcanismName = "Cryovolcanism";
        gravityAsistName = "Gravity Assit";
        airCompName = "Air Composition";
        miningName = "Mining";
        marsName = "Mars";
        asteroidBeltName = "Asteroid Belt";
        jupiterName = "Jupiter";
        jupiterMoonsName = "Jupiter's Moons";
        saturnName = "Saturn";
        titanName = "Titan";

        // FOR TESTING PURPOSES SET TO TRUE AT BEGINNING
        isPlantUnlocked = true;
        isCMEUnlocked = true;
        isDwarfPlanetUnlocked = true;
        isCryovolcanismUnlocked = true;
        isGravityAssitUnlocked = true;
        isAirCompUnlocked = true;
        isMiningUnlocked = true;
        isMarsUnlocked = true;
        isAsteroidBeltUnlocked = true;
        isJupiterUnlocked = true;
        isJupiterMoonsUnlocked = true;
        isSaturnUnlocked = true;
        isSaturnUnlocked = true;
        isTitanUnlocked = true;

        // set display text from the scriptable file
        plantText.GetComponent<TextMeshProUGUI>().text = plantMissionText.missionText;
        cmeText.GetComponent<TextMeshProUGUI>().text = cmeMissionText.missionText;
        dwarfPlanetText.GetComponent<TextMeshProUGUI>().text = dwarfPlanetMissionText.missionText;
        cryovolcanismText.GetComponent<TextMeshProUGUI>().text = cryovolcanismMissionText.missionText;
        gravityAssitText.GetComponent<TextMeshProUGUI>().text = gravityAssitMissionText.missionText;
        airCompText.GetComponent<TextMeshProUGUI>().text = airCompMissionText.missionText;
        miningText.GetComponent<TextMeshProUGUI>().text = miningMissionText.missionText;
        marsText.GetComponent<TextMeshProUGUI>().text = marsMissionText.missionText;
        asteroidBeltText.GetComponent<TextMeshProUGUI>().text = asteroidBeltMissionText.missionText;
        jupiterText.GetComponent<TextMeshProUGUI>().text = jupiterMissionText.missionText;
        jupiterMoonsText.GetComponent<TextMeshProUGUI>().text = jupiterMoonsMissionText.missionText;
        saturnText.GetComponent<TextMeshProUGUI>().text = saturnMissionText.missionText;
        titanText.GetComponent<TextMeshProUGUI>().text = titanMissionText.missionText;
    }


    private void Update()
    {
        // unlock mission logs as time progresses
        UnlockMissionLog(isPlantUnlocked, plantButton, plantName);
        UnlockMissionLog(isCMEUnlocked, cmeButton, cmeName);
        UnlockMissionLog(isDwarfPlanetUnlocked, dwarfPlanetButton, dwarfPlanetName);
        UnlockMissionLog(isCryovolcanismUnlocked, cryovolcansimButton, cryovolcanismName);
        UnlockMissionLog(isGravityAssitUnlocked, gravityAssitButton, gravityAsistName);
        UnlockMissionLog(isAirCompUnlocked, airCompButton, airCompName);
        UnlockMissionLog(isMiningUnlocked, miningButton, miningName);
        UnlockMissionLog(isMarsUnlocked, marsButton, marsName);
        UnlockMissionLog(isAsteroidBeltUnlocked, asteroidBeltButton, asteroidBeltName);
        UnlockMissionLog(isJupiterUnlocked, jupiterButton, jupiterName);
        UnlockMissionLog(isJupiterMoonsUnlocked, jupiterMoonsButton, jupiterMoonsName);
        UnlockMissionLog(isSaturnUnlocked, saturnButton, saturnName);
        UnlockMissionLog(isTitanUnlocked, titanButton, titanName);
    }

    public void UnlockMissionLog(bool isLogUnlocked, GameObject logButton, string logName)
    {
        // unlock mission logs
        if (!isLogUnlocked)
        {
            logButton.GetComponent<Button>().interactable = false;
            logButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disabled";
        }
        else { 
            logButton.GetComponent<Button>().interactable = true;
            logButton.GetComponentInChildren<TextMeshProUGUI>().text = logName;
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        terminalText.SetActive(true);
        gameManager.StartGameFromPause();
    }

    public void PauseGame()
    {
        terminalText.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // freeze game
        gameManager.StopTimePause();
    }

    public void OpenReferenceMenu()
    {
        // open reference menu
        referenceOpened = true;
        pauseMenuUI.SetActive(false);
        referenceMenuUI.SetActive(true);
    }

    public void CloseReferenceMenu()
    {
        // open close menu
        referenceOpened = false;
        referenceMenuUI.SetActive(false);
        referenceMenuImagesUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        // close mission log info
        scrollRectContainer.SetActive(false);
        missionLogTextUI.SetActive(true); // turn on reference UI text
        missionLogOutline.SetActive(false); // turn offf internal screen with scrollbar
    }

    public void OpenMissionLog(GameObject missionText)
    {
        // when opening a new (or existing) log first turn off all other texts
        plantText.SetActive(false);
        cmeText.SetActive(false);
        dwarfPlanetText.SetActive(false);
        cryovolcanismText.SetActive(false);
        gravityAssitText.SetActive(false);
        airCompText.SetActive(false);
        miningText.SetActive(false);
        marsText.SetActive(false);
        asteroidBeltText.SetActive(false);
        jupiterText.SetActive(false);
        jupiterMoonsText.SetActive(false);
        saturnText.SetActive(false);
        titanText.SetActive(false);

        // display rect container and scrollbar
        scrollRectContainer.SetActive(true);
        missionLogOutline.SetActive(true); // turn on internal screen with scrollbar
        referenceMenuImagesUI.SetActive(true);

        contentDisplay.content = missionText.GetComponent<RectTransform>(); // display text
        missionText.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("TODO: RESTARTING GAME");
        Debug.Log("TODO: POPUOP WARNING ABOUT PROGRESS");
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitToMainMenu()
    {
        Debug.Log("EXIT GAME TO MAIN MENU");
        Debug.Log("TODO: POPUP WARNING ABOUT PROGRESS");
        SceneManager.LoadScene("MainMenu");
    }
}
