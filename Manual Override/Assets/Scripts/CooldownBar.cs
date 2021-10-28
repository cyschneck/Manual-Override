using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    // Controls the cooldown bar for each button
    
    [Header("Reference")]
    private GameManager gameManager;

    [Header("Cooldown Bars")]
    public Dictionary<string, bool> cooldownDict = new Dictionary<string, bool>(); // store the name of the cooldown (to pass to coroutine) and whether cooldown is active
    public float engineCooldown;
    public GameObject engineCooldownBar;
    public GameObject germinateSeedsCooldownBar;
    public GameObject plantSeedsCooldownBar;
    public GameObject treatPlantsCooldownBar;
    public GameObject performElectrolysisCooldownBar;
    public GameObject performSabatierCooldownBar;
    public GameObject reverseEngineCooldownBar;
    public GameObject assembleRobotsCooldownBar;
    public GameObject dismantleRobotsCooldownBar;
    public GameObject scanCooldownBar;
    public GameObject deployMiningRobotsCooldownBar;
    public GameObject assembleBatteryCooldownBar;
    public GameObject dismantleBatteryCooldownBar;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        engineCooldown = 15.0f;
        // set up dictionary to track cooldowns triggered
        cooldownDict.Add("engineCooldown", false);
        cooldownDict.Add("germinateSeedsCooldown", false);
        cooldownDict.Add("plantSeedsCooldown", false);
        cooldownDict.Add("treatPlantsCooldown", false);
        cooldownDict.Add("performElectrolysisCooldown", false);
        cooldownDict.Add("performSabatierCooldown", false);
        cooldownDict.Add("reverseEngineCooldown", false);
        cooldownDict.Add("assembleRobotsCooldown", false);
        cooldownDict.Add("dismantleRobotsCooldown", false);
        cooldownDict.Add("scanCooldown", false);
        cooldownDict.Add("deployMiningRobotsCooldown", false);
        cooldownDict.Add("assembleBatteryCooldown", false);
        cooldownDict.Add("dismantleBatteryCooldown", false);

        // set cooldown bar for each button with cooldown
        engineCooldownBar = gameManager.engineOnOffButton.transform.GetChild(1).gameObject;
        germinateSeedsCooldownBar = gameManager.germinateSeedsButton.transform.GetChild(2).gameObject;
        plantSeedsCooldownBar = gameManager.plantSeedsButton.transform.GetChild(2).gameObject;
        treatPlantsCooldownBar = gameManager.treatPlantsButton.transform.GetChild(2).gameObject;
        performElectrolysisCooldownBar = gameManager.performElectrolysisButton.transform.GetChild(2).gameObject;
        performSabatierCooldownBar = gameManager.performSabatierButton.transform.GetChild(2).gameObject;
        reverseEngineCooldownBar = gameManager.reverseEngineButton.transform.GetChild(2).gameObject;
        assembleRobotsCooldownBar = gameManager.assembleRobotsButton.transform.GetChild(2).gameObject;
        dismantleRobotsCooldownBar = gameManager.dismantleRobotsButton.transform.GetChild(2).gameObject;
        scanCooldownBar = gameManager.scanButton.transform.GetChild(2).gameObject;
        deployMiningRobotsCooldownBar = gameManager.deployMiningRobotsButton.transform.GetChild(2).gameObject;
        assembleBatteryCooldownBar = gameManager.assembleBatteryButton.transform.GetChild(2).gameObject;
        dismantleBatteryCooldownBar = gameManager.dismantleBatteryButton.transform.GetChild(2).gameObject;

        // on start up, set all cooldown sliders to 0 so they are not visible
        engineCooldownBar.GetComponent<Slider>().value = 0;
        germinateSeedsCooldownBar.GetComponent<Slider>().value = 0;
        plantSeedsCooldownBar.GetComponent<Slider>().value = 0;
        treatPlantsCooldownBar.GetComponent<Slider>().value = 0;
        performElectrolysisCooldownBar.GetComponent<Slider>().value = 0;
        performSabatierCooldownBar.GetComponent<Slider>().value = 0;
        reverseEngineCooldownBar.GetComponent<Slider>().value = 0;
        assembleRobotsCooldownBar.GetComponent<Slider>().value = 0;
        dismantleRobotsCooldownBar.GetComponent<Slider>().value = 0;
        scanCooldownBar.GetComponent<Slider>().value = 0;
        deployMiningRobotsCooldownBar.GetComponent<Slider>().value = 0;
        assembleBatteryCooldownBar.GetComponent<Slider>().value = 0;
        dismantleBatteryCooldownBar.GetComponent<Slider>().value = 0;
    }

    private void FixedUpdate()
    {
        // cooldown effects make the button not able to be interacted with
        if (!gameManager.gameIsPaused)
        {
            gameManager.engineOnOffButton.GetComponent<Button>().interactable = !cooldownDict["engineCooldown"];
            gameManager.germinateSeedsButton.GetComponent<Button>().interactable = !cooldownDict["germinateSeedsCooldown"];
            gameManager.plantSeedsButton.GetComponent<Button>().interactable = !cooldownDict["plantSeedsCooldown"];
            gameManager.treatPlantsButton.GetComponent<Button>().interactable = !cooldownDict["treatPlantsCooldown"];
            gameManager.performElectrolysisButton.GetComponent<Button>().interactable = !cooldownDict["performElectrolysisCooldown"];
            gameManager.performSabatierButton.GetComponent<Button>().interactable = !cooldownDict["performSabatierCooldown"];
            gameManager.reverseEngineButton.GetComponent<Button>().interactable = !cooldownDict["reverseEngineCooldown"];
            gameManager.assembleRobotsButton.GetComponent<Button>().interactable = !cooldownDict["assembleRobotsCooldown"];
            gameManager.dismantleRobotsButton.GetComponent<Button>().interactable = !cooldownDict["dismantleRobotsCooldown"];
            gameManager.scanButton.GetComponent<Button>().interactable = !cooldownDict["scanCooldown"];
            gameManager.deployMiningRobotsButton.GetComponent<Button>().interactable = !cooldownDict["deployMiningRobotsCooldown"];
            gameManager.assembleBatteryButton.GetComponent<Button>().interactable = !cooldownDict["assembleBatteryCooldown"];
            gameManager.dismantleBatteryButton.GetComponent<Button>().interactable = !cooldownDict["dismantleBatteryCooldown"];
        }
    }

    public IEnumerator TriggerCooldown(float cooldownTime, string keyCooldownDict)
    {
        //Debug.Log("COOLDOWN TRIGGERED FOR: " + keyCooldownDict + " for " + cooldownTime);
        // set cooldown as active, wait until the cooldown timer is done, then set to true
        cooldownDict[keyCooldownDict] = true;

        // finish cooldown and release cooldown
        yield return new WaitForSeconds(cooldownTime);
        cooldownDict[keyCooldownDict] = false;
    }

    public IEnumerator UpdateCooldownBar(float cooldownTime, GameObject cooldownBar)
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

}
