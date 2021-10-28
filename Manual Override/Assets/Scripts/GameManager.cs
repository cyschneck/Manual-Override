using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum roomType { main, science, engineering }
public class GameManager : MonoBehaviour
{
    // Game manger controls: buttons on UI, , button interactably, start/stop time, change rooms

    [Header("Reference")]
    private StatsManager statsManager;
    private CooldownBar cooldownBar;
    public bool gameIsPaused = false;
    public float currentTime;

    [Header("Engine Display")]
    public bool isEngineOn;
    public TextMeshProUGUI engineText;

    [Header("Room Button Options")]
    public GameObject scienceDeptButton;
    public bool isScienceDepWorking;
    public GameObject engineeringDeptButton;
    public bool isEngineeringDeptWorking;
    public roomType whichRoom = roomType.main;
    public GameObject engineOnOffButton;
    public GameObject germinateSeedsButton;
    public GameObject plantSeedsButton;
    public GameObject treatPlantsButton;
    public GameObject performElectrolysisButton;
    public GameObject performSabatierButton;
    public GameObject stopEngineButton;
    public GameObject assembleRobotsButton;
    public GameObject dismantleRobotsButton;
    public GameObject scanButton;
    public GameObject deployMiningRobotsButton;
    public GameObject assembleBatteryButton;
    public GameObject dismantleBatteryButton;
    public GameObject continueOptionButton;
    public GameObject yesOptionButton;
    public GameObject noOptionButton;

    [Header("Department UI")]
    public GameObject scienceDepartmentUI;
    public GameObject engineeringDepartmentUI;
    public GameObject returnToMainDepartment;

    [Header("Text to Display")]
    private TerminalTextManager terminalTextManager;
    public TextToDisplay openingText1;
    public TextToDisplay openingText2;
    public TextToDisplay turnOnEngineText;
    public TextToDisplay turnOffEngineText;
    public TextToDisplay enterScienceDeptText;
    public TextToDisplay enterEngineeringDeptText;
    public TextToDisplay germinateSeedsText;
    public TextToDisplay plantSeedsText;
    public TextToDisplay treatPlantsText;
    public TextToDisplay performElectrolysisText;
    public TextToDisplay performSabatierText;
    public TextToDisplay deployMiningRobotsText;
    public TextToDisplay stopEngineText;
    public TextToDisplay assembleRobotsText;
    public TextToDisplay dismantleRobotsText;
    public TextToDisplay startScanText;
    public TextToDisplay assembleBatteryText;
    public TextToDisplay dismantleBatteryText;
    public TextToDisplay continueOptionText;
    public TextToDisplay yesOptionText;
    public TextToDisplay noOptionText;

    public void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();
        cooldownBar = GameObject.Find("CooldownManager").GetComponent<CooldownBar>();

        // track time in the game
        currentTime = 0.0f;
        // start of the game, set all values to default
        statsManager.SetDefaultValues();

        whichRoom = roomType.main;

        // set up departments at the beginning of the game
        isScienceDepWorking = true; // by default = false
        isEngineeringDeptWorking = true; // by default = false
        isEngineOn = false;
        engineText.text = "Turn on Engine";

        // trigger the start of the terminal to write opening credits
        terminalTextManager.terminalTextUI.text = ""; // start with clear terminal
        terminalTextManager.startWriteText(openingText1.text);
        terminalTextManager.startWriteText(openingText2.text);
    }

    private void FixedUpdate()
    {
        // update time over time
        currentTime += 1.0f * Time.deltaTime;

        if (whichRoom == roomType.main)
        {
            // if in main room, set UI elements back to default
            scienceDepartmentUI.SetActive(false);
            engineeringDepartmentUI.SetActive(false);
            returnToMainDepartment.SetActive(false);
        }

        // turn off display option for science department if it is not working
        if (!gameIsPaused) // only update science/engineering buttons when not paused
        {
            if (!isScienceDepWorking)
            {
                scienceDeptButton.GetComponent<Button>().interactable = false;
                scienceDeptButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disabled";
            }
            else
            {
                scienceDeptButton.GetComponent<Button>().interactable = true;
                scienceDeptButton.GetComponentInChildren<TextMeshProUGUI>().text = "Science Dept";
            }

            // turn off display option for engineering department if it is not working
            if (!isEngineeringDeptWorking)
            {
                engineeringDeptButton.GetComponent<Button>().interactable = false;
                engineeringDeptButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disabled";
            }
            else
            {
                engineeringDeptButton.GetComponent<Button>().interactable = true;
                engineeringDeptButton.GetComponentInChildren<TextMeshProUGUI>().text = "Engineering Dept";
            }
        }
    }

    public void DisableOrEnableAllTermianlButtonsInteractable(bool isButtonInteractable)
    {
        // remove interactable buttons from the terminal page when game is paused
        engineOnOffButton.GetComponent<Button>().interactable = isButtonInteractable;
        germinateSeedsButton.GetComponent<Button>().interactable = isButtonInteractable;
        plantSeedsButton.GetComponent<Button>().interactable = isButtonInteractable;
        treatPlantsButton.GetComponent<Button>().interactable = isButtonInteractable;
        performElectrolysisButton.GetComponent<Button>().interactable = isButtonInteractable;
        performSabatierButton.GetComponent<Button>().interactable = isButtonInteractable;
        stopEngineButton.GetComponent<Button>().interactable = isButtonInteractable;
        assembleRobotsButton.GetComponent<Button>().interactable = isButtonInteractable;
        dismantleRobotsButton.GetComponent<Button>().interactable = isButtonInteractable;
        scanButton.GetComponent<Button>().interactable = isButtonInteractable;
        deployMiningRobotsButton.GetComponent<Button>().interactable = isButtonInteractable;
        assembleBatteryButton.GetComponent<Button>().interactable = isButtonInteractable;
        dismantleBatteryButton.GetComponent<Button>().interactable = isButtonInteractable;
    }

    public void StopTimePause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void StartGameFromPause()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void SwitchEngineState()
    {
        // if engine on, turn it off (else turn on)
        if (isEngineOn)
        {
            // turns engine off
            engineText.text = "Turn on Engine";
            terminalTextManager.startWriteText(turnOffEngineText.text);
            isEngineOn = false;
            StartCoroutine(statsManager.SetRocketThrust(isEngineOn));
        }
        else
        {
            // turns engine on
            engineText.text = "Turn off Engine";
            terminalTextManager.startWriteText(turnOnEngineText.text);
            isEngineOn = true;
            StartCoroutine(statsManager.SetRocketThrust(isEngineOn));
        }
        // update slider for cooldown
        StartCoroutine(cooldownBar.UpdateCooldownBar(cooldownBar.engineCooldown, cooldownBar.engineCooldownBar));
        StartCoroutine(cooldownBar.TriggerCooldown(cooldownBar.engineCooldown, "engineCooldown")); // wait x seconds before ending cooldown
    }

    public void EnterScienceDepartment()
    {
        // ENTER SCIENCE DEPARTMENT
        whichRoom = roomType.science;
        scienceDeptButton.SetActive(false);
        engineeringDeptButton.SetActive(true);
        returnToMainDepartment.SetActive(true);
        scienceDepartmentUI.SetActive(true);
        engineeringDepartmentUI.SetActive(false);

        // enter the engineering department
        terminalTextManager.startWriteText(enterScienceDeptText.text);
    }

    public void EnterEngineeringDepartment()
    {
        // ENTER ENGINEERING DEPARTMENT
        whichRoom = roomType.engineering;
        scienceDeptButton.SetActive(true);
        engineeringDeptButton.SetActive(false);
        returnToMainDepartment.SetActive(true);
        scienceDepartmentUI.SetActive(false);
        engineeringDepartmentUI.SetActive(true);

        // enter the engineering department
        terminalTextManager.startWriteText(enterEngineeringDeptText.text);
    }

    public void ReturnToMainTerminal()
    {
        whichRoom = roomType.main;
        returnToMainDepartment.SetActive(false);
        engineeringDeptButton.SetActive(true);
        scienceDeptButton.SetActive(true);
    }

    public void GerminateSeeds()
    {
        if (!statsManager.DoesNotHaveResources(germinateSeedsText))
        {
            // germinate seeds in the science department
            terminalTextManager.startWriteText(germinateSeedsText.text);
            statsManager.UpdateStaticValues(germinateSeedsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(germinateSeedsText.cooldownTime, cooldownBar.germinateSeedsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(germinateSeedsText.cooldownTime, "germinateSeedsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void PlantSeeds()
    {
        if (!statsManager.DoesNotHaveResources(plantSeedsText))
        {
            // plants seeds in the science department
            terminalTextManager.startWriteText(plantSeedsText.text);
            statsManager.UpdateStaticValues(plantSeedsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(plantSeedsText.cooldownTime, cooldownBar.plantSeedsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(plantSeedsText.cooldownTime, "plantSeedsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void TreatPlants()
    {
        if (!statsManager.DoesNotHaveResources(treatPlantsText))
        {
            // treat seeds in the science department
            terminalTextManager.startWriteText(treatPlantsText.text);
            statsManager.UpdateStaticValues(treatPlantsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(treatPlantsText.cooldownTime, cooldownBar.treatPlantsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(treatPlantsText.cooldownTime, "treatPlantsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void PerformElectrolysis()
    {
        if (!statsManager.DoesNotHaveResources(performElectrolysisText))
        {
            // preform electrolysis in the science department
            terminalTextManager.startWriteText(performElectrolysisText.text);
            statsManager.UpdateStaticValues(performElectrolysisText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(performElectrolysisText.cooldownTime, cooldownBar.performElectrolysisCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(performElectrolysisText.cooldownTime, "performElectrolysisCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void PerformSabatierReaction()
    {
        if (!statsManager.DoesNotHaveResources(performSabatierText))
        {
            // preform electrolysis in the science department
            terminalTextManager.startWriteText(performSabatierText.text);
            statsManager.UpdateStaticValues(performSabatierText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(performSabatierText.cooldownTime, cooldownBar.performSabatierCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(performSabatierText.cooldownTime, "performSabatierCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void StopEngine()
    {
        // reverse the direction of thrust of engine and stop the ship
        if (!statsManager.DoesNotHaveResources(stopEngineText))
        {
            if (statsManager.isEngineChangingSpeed)
            {
                // prevent engine switch when engine is one
                terminalTextManager.startWriteText("Engine in motion, cannot switch direction");
            }
            else
            {
                terminalTextManager.startWriteText(stopEngineText.text);

                // update slider for cooldown
                StartCoroutine(cooldownBar.UpdateCooldownBar(stopEngineText.cooldownTime, cooldownBar.stopEngineCooldownBar));
                StartCoroutine(cooldownBar.TriggerCooldown(stopEngineText.cooldownTime, "stopEngineCooldown")); // wait x seconds before ending cooldown
            }
        }
    }

    public void AssembleRobots()
    {
        if (!statsManager.DoesNotHaveResources(assembleRobotsText))
        {
            // manufacture robot in the engineering department
            terminalTextManager.startWriteText(assembleRobotsText.text);
            statsManager.UpdateStaticValues(assembleRobotsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(assembleRobotsText.cooldownTime, cooldownBar.assembleRobotsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(assembleRobotsText.cooldownTime, "assembleRobotsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void DismantleRobots()
    {
        if (!statsManager.DoesNotHaveResources(dismantleRobotsText))
        {
            // manufacture robot in the engineering department
            terminalTextManager.startWriteText(dismantleRobotsText.text);
            statsManager.UpdateStaticValues(dismantleRobotsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(dismantleRobotsText.cooldownTime, cooldownBar.dismantleRobotsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(dismantleRobotsText.cooldownTime, "dismantleRobotsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void StartScan()
    {
        if (!statsManager.DoesNotHaveResources(startScanText))
        {
            // manufacture robot in the engineering department
            terminalTextManager.startWriteText(startScanText.text);
            statsManager.UpdateStaticValues(startScanText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(startScanText.cooldownTime, cooldownBar.scanCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(startScanText.cooldownTime, "scanCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void DeployMiningRobots()
    {
        if (!statsManager.DoesNotHaveResources(deployMiningRobotsText))
        {
            // deploy mining robots in engineering department
            terminalTextManager.startWriteText(deployMiningRobotsText.text);
            statsManager.UpdateStaticValues(deployMiningRobotsText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(deployMiningRobotsText.cooldownTime, cooldownBar.deployMiningRobotsCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(deployMiningRobotsText.cooldownTime, "deployMiningRobotsCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void AssembleBattery()
    {
        if (!statsManager.DoesNotHaveResources(assembleBatteryText))
        {
            // manufacture battery in the engineering department
            terminalTextManager.startWriteText(assembleBatteryText.text);
            statsManager.UpdateStaticValues(assembleBatteryText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(assembleBatteryText.cooldownTime, cooldownBar.assembleBatteryCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(assembleBatteryText.cooldownTime, "assembleBatteryCooldown")); // wait x seconds before ending cooldown
        }
    }

    public void DismantleBattery()
    {
        if (!statsManager.DoesNotHaveResources(dismantleBatteryText))
        {
            // manufacture robot in the engineering department
            terminalTextManager.startWriteText(dismantleBatteryText.text);
            statsManager.UpdateStaticValues(dismantleBatteryText);
            // update slider for cooldown
            StartCoroutine(cooldownBar.UpdateCooldownBar(dismantleBatteryText.cooldownTime, cooldownBar.dismantleBatteryCooldownBar));
            StartCoroutine(cooldownBar.TriggerCooldown(dismantleBatteryText.cooldownTime, "dismantleBatteryCooldown")); // wait x seconds before ending cooldown
        }
    }
}
