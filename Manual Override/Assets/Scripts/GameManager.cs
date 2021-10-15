using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum roomType { main, science, engineering }
public class GameManager : MonoBehaviour
{
    [Header("Reference")]
    private StatsManager statsManager;
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
    public GameObject assembleRobotsButton;
    public GameObject dismantleRobotsButton;
    public GameObject scanButton;
    public GameObject deployMiningRobotsButton;
    public GameObject assembleBatteryButton;
    public GameObject dismantleBatteryButton;

    [Header("Department UI")]
    public GameObject scienceDepartmentUI;
    public GameObject engineeringDepartmentUI;
    public GameObject returnToMainDepartment;

    [Header("Cooldown")]
    public Dictionary<string, bool> cooldownDict = new Dictionary<string, bool>(); // store the name of the cooldown (to pass to coroutine) and whether cooldown is active
    public float engineCooldown = 15.0f;
    private GameObject engineCooldownBar;
    private GameObject germinateSeedsCooldownBar;
    private GameObject plantSeedsCooldownBar;
    private GameObject treatPlantsCooldownBar;
    private GameObject performElectrolysisCooldownBar;
    private GameObject assembleRobotsCooldownBar;
    private GameObject dismantleRobotsCooldownBar;
    private GameObject scanCooldownBar;
    private GameObject deployMiningRobotsCooldownBar;
    private GameObject assembleBatteryCooldownBar;
    private GameObject dismantleBatteryCooldownBar;

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
    public TextToDisplay deployMiningRobotsText;
    public TextToDisplay assembleRobotsText;
    public TextToDisplay dismantleRobotsText;
    public TextToDisplay startScanText;
    public TextToDisplay assembleBatteryText;
    public TextToDisplay dismantleBatteryText;

    public void Start()
    {
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();


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

        // set up dictionary to track cooldowns triggered
        cooldownDict.Add("engineCooldown", false);
        cooldownDict.Add("germinateSeedsCooldown", false);
        cooldownDict.Add("plantSeedsCooldown", false);
        cooldownDict.Add("treatPlantsCooldown", false);
        cooldownDict.Add("performElectrolysisCooldown", false);
        cooldownDict.Add("assembleRobotsCooldown", false);
        cooldownDict.Add("dismantleRobotsCooldown", false);
        cooldownDict.Add("scanCooldown", false);
        cooldownDict.Add("deployMiningRobotsCooldown", false);
        cooldownDict.Add("assembleBatteryCooldown", false);
        cooldownDict.Add("dismantleBatteryCooldown", false);

        // set cooldown bar for each button with cooldown
        engineCooldownBar = engineOnOffButton.transform.GetChild(1).gameObject;
        germinateSeedsCooldownBar = germinateSeedsButton.transform.GetChild(1).gameObject;
        plantSeedsCooldownBar = plantSeedsButton.transform.GetChild(1).gameObject;
        treatPlantsCooldownBar = treatPlantsButton.transform.GetChild(1).gameObject;
        performElectrolysisCooldownBar = performElectrolysisButton.transform.GetChild(1).gameObject;
        assembleRobotsCooldownBar = assembleRobotsButton.transform.GetChild(1).gameObject;
        dismantleRobotsCooldownBar = dismantleRobotsButton.transform.GetChild(1).gameObject;
        scanCooldownBar = scanButton.transform.GetChild(1).gameObject;
        deployMiningRobotsCooldownBar = deployMiningRobotsButton.transform.GetChild(1).gameObject;
        assembleBatteryCooldownBar = assembleBatteryButton.transform.GetChild(1).gameObject;
        dismantleBatteryCooldownBar = dismantleBatteryButton.transform.GetChild(1).gameObject;

        // on start up, set all cooldown sliders to 0 so they are not visible
        engineCooldownBar.GetComponent<Slider>().value = 0;
        germinateSeedsCooldownBar.GetComponent<Slider>().value = 0;
        plantSeedsCooldownBar.GetComponent<Slider>().value = 0;
        treatPlantsCooldownBar.GetComponent<Slider>().value = 0;
        performElectrolysisCooldownBar.GetComponent<Slider>().value = 0;
        assembleRobotsCooldownBar.GetComponent<Slider>().value = 0;
        dismantleRobotsCooldownBar.GetComponent<Slider>().value = 0;
        scanCooldownBar.GetComponent<Slider>().value = 0;
        deployMiningRobotsCooldownBar.GetComponent<Slider>().value = 0;
        assembleBatteryCooldownBar.GetComponent<Slider>().value = 0;
        dismantleBatteryCooldownBar.GetComponent<Slider>().value = 0;
    }

    private void Update()
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

        // cooldown effects make the button not able to be interacted with
        if (!gameIsPaused)
        {
            engineOnOffButton.GetComponent<Button>().interactable = !cooldownDict["engineCooldown"];
            germinateSeedsButton.GetComponent<Button>().interactable = !cooldownDict["germinateSeedsCooldown"];
            plantSeedsButton.GetComponent<Button>().interactable = !cooldownDict["plantSeedsCooldown"];
            treatPlantsButton.GetComponent<Button>().interactable = !cooldownDict["treatPlantsCooldown"];
            performElectrolysisButton.GetComponent<Button>().interactable = !cooldownDict["performElectrolysisCooldown"];
            assembleRobotsButton.GetComponent<Button>().interactable = !cooldownDict["assembleRobotsCooldown"];
            dismantleRobotsButton.GetComponent<Button>().interactable = !cooldownDict["dismantleRobotsCooldown"];
            scanButton.GetComponent<Button>().interactable = !cooldownDict["scanCooldown"];
            deployMiningRobotsButton.GetComponent<Button>().interactable = !cooldownDict["deployMiningRobotsCooldown"];
            assembleBatteryButton.GetComponent<Button>().interactable = !cooldownDict["assembleBatteryCooldown"];
            dismantleBatteryButton.GetComponent<Button>().interactable = !cooldownDict["dismantleBatteryCooldown"];
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
            StartCoroutine(statsManager.SetEngineSpeed(isEngineOn));
        }
        else
        {
            // turns engine on
            engineText.text = "Turn off Engine";
            terminalTextManager.startWriteText(turnOnEngineText.text);
            isEngineOn = true;
            StartCoroutine(statsManager.SetEngineSpeed(isEngineOn));
        }
        // update slider for cooldown
        StartCoroutine(UpdateCooldownBar(engineCooldown, engineCooldownBar));
        StartCoroutine(TriggerCooldown(engineCooldown, "engineCooldown")); // wait x seconds before ending cooldown
    }

    private IEnumerator TriggerCooldown(float cooldownTime, string keyCooldownDict)
    {
        //Debug.Log("COOLDOWN TRIGGERED FOR: " + keyCooldownDict + " for " + cooldownTime);
        // set cooldown as active, wait until the cooldown timer is done, then set to true
        cooldownDict[keyCooldownDict] = true;

        // finish cooldown and release cooldown
        yield return new WaitForSeconds(cooldownTime);
        cooldownDict[keyCooldownDict] = false;
    }

    private IEnumerator UpdateCooldownBar(float cooldownTime, GameObject cooldownBar)
    {
        // shrink overlay cooldown bar overtime
        Slider cooldownSlider = cooldownBar.GetComponent<Slider>();
        float elapsedTime = 0.0f;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            cooldownSlider.value = Mathf.Lerp(1, 0, elapsedTime / cooldownTime);
            yield return null;
        }
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
            StartCoroutine(UpdateCooldownBar(germinateSeedsText.cooldownTime, germinateSeedsCooldownBar));
            StartCoroutine(TriggerCooldown(germinateSeedsText.cooldownTime, "germinateSeedsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(plantSeedsText.cooldownTime, plantSeedsCooldownBar));
            StartCoroutine(TriggerCooldown(plantSeedsText.cooldownTime, "plantSeedsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(treatPlantsText.cooldownTime, treatPlantsCooldownBar));
            StartCoroutine(TriggerCooldown(treatPlantsText.cooldownTime, "treatPlantsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(performElectrolysisText.cooldownTime, performElectrolysisCooldownBar));
            StartCoroutine(TriggerCooldown(performElectrolysisText.cooldownTime, "performElectrolysisCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(assembleRobotsText.cooldownTime, assembleRobotsCooldownBar));
            StartCoroutine(TriggerCooldown(assembleRobotsText.cooldownTime, "assembleRobotsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(dismantleRobotsText.cooldownTime, dismantleRobotsCooldownBar));
            StartCoroutine(TriggerCooldown(dismantleRobotsText.cooldownTime, "dismantleRobotsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(startScanText.cooldownTime, scanCooldownBar));
            StartCoroutine(TriggerCooldown(startScanText.cooldownTime, "scanCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(deployMiningRobotsText.cooldownTime, deployMiningRobotsCooldownBar));
            StartCoroutine(TriggerCooldown(deployMiningRobotsText.cooldownTime, "deployMiningRobotsCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(assembleBatteryText.cooldownTime, assembleBatteryCooldownBar));
            StartCoroutine(TriggerCooldown(assembleBatteryText.cooldownTime, "assembleBatteryCooldown")); // wait x seconds before ending cooldown
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
            StartCoroutine(UpdateCooldownBar(dismantleBatteryText.cooldownTime, dismantleBatteryCooldownBar));
            StartCoroutine(TriggerCooldown(dismantleBatteryText.cooldownTime, "dismantleBatteryCooldown")); // wait x seconds before ending cooldown
        }
    }
}
