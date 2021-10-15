using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    // controls updating the stats on the home page, science department, and engineering department
    [Header("Reference")]
    private GameManager gameManager;
    private StatsForStringValues stringStatsValues;
    private TerminalTextManager terminalTextManager;

    [Header("Static Stats")]
    public float energyCellAmount;
    public float waterAmount;
    public float robotsAmount;
    public float plantsAmount;
    public float engineSpeedValue;
    public float distanceToTitanValue;
    public float nitrogenValue;
    public float oxygenValue;
    public float carbonDioxdeValue;
    public float hydrogenValue;
    public float seedsAmount;
    public float chemicalsAmount;
    public float copperWireAmount;
    public float metalAmount;
    public float deadBatteryAmount;
    public float distanceToTitanAmount;
    public float distanceChangeDelay;
    public float timeBetweenDelay; // how often to update distance
    private bool notEnoughResourcesTriggered; // trigger a text value if any values in the list are zero
    private int idleEngineSpeed = 3000;

    [Header("Ship Main Stats")]
    public TextMeshProUGUI energyCellStats;
    public TextMeshProUGUI waterStats;
    public TextMeshProUGUI robotsStats;
    public TextMeshProUGUI plantStats;
    public TextMeshProUGUI engineSpeedStats;
    public TextMeshProUGUI distanceToTitanStats;
    public TextMeshProUGUI airCompStats;

    [Header("Ship Science Stats")]
    public TextMeshProUGUI chemicalsStats;
    public TextMeshProUGUI seedStats;

    [Header("Ship Engineering Stats")]
    public TextMeshProUGUI copperWireStats;
    public TextMeshProUGUI metalStats;
    public TextMeshProUGUI deadBatteriesStats;

    private Dictionary<string, string> textDictionary = new Dictionary<string, string>();


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();
        stringStatsValues = GameObject.Find("StatsManager").GetComponent<StatsForStringValues>();

        // dictionary to store displayed version of stats name (chemical = "avaiable chemicals")
        textDictionary.Add("energy cells", "Energy Cells");
        textDictionary.Add("water", "water");
        textDictionary.Add("robots", "Robotics");
        textDictionary.Add("plants", "Plants");
        textDictionary.Add("seeds", "Seeds");
        textDictionary.Add("chemicals", "Avaliable Chemicals");
        textDictionary.Add("nitrogen", "Nitrogen Gas");
        textDictionary.Add("oxygen", "Oxygen Gas");
        textDictionary.Add("co2", "Carbon Dioxide Gas");
        textDictionary.Add("hydrogen", "Hydrogen Gas");
        textDictionary.Add("copper wire", "Copper Wire");
        textDictionary.Add("metal", "Processed Metal");
        textDictionary.Add("dead battery", "Dead Batteries");

        // how often to update distance to titan
        distanceChangeDelay = 4.0f; // update every x seconds
        timeBetweenDelay = 0.0f;
    }

    private void Update()
    {
        // update values as they are updated
        SetStatValue(energyCellStats, "energy cells", energyCellAmount);
        SetStatValue(waterStats, "water", waterAmount);
        SetStatValue(robotsStats, "robots", robotsAmount);
        SetStatValue(plantStats, "plants", plantsAmount);
        SetStatValue(seedStats, "seeds", seedsAmount);
        SetStatValue(chemicalsStats, "chemicals", chemicalsAmount);
        SetStatValue(copperWireStats, "copper wire", copperWireAmount);
        SetStatValue(metalStats, "metal", metalAmount);
        SetStatValue(deadBatteriesStats, "dead battery", deadBatteryAmount);
        SetEngineSpeed(engineSpeedValue);
        SetTitanDistance(distanceToTitanAmount);
        SetAirComp(nitrogenValue, oxygenValue, carbonDioxdeValue, hydrogenValue);

        // calculate the distance every second while the engine is on, randomly change speed by 10%
        if (gameManager.isEngineOn && !gameManager.gameIsPaused)
        {
            timeBetweenDelay += 1.0f * Time.deltaTime;
            if (timeBetweenDelay >= distanceChangeDelay)
            {
                // update every x seconds
                //Debug.Log("Displayed every " + distanceChangeDelay + " seconds");
                ReduceRemainingDistanceToTitan();
                RandomlyIncreaseDecreaseEngineSpeed();
                timeBetweenDelay = 0.0f;
            }
        }
        if (!gameManager.isEngineOn)
        {
            timeBetweenDelay = 0.0f; // reset when not in use
        }

    }

    public void SetDefaultValues()
    {
        // set default values for strings
        stringStatsValues.SetDefaultValuesStrings();

        // set default values for integers
        SetStatValue(energyCellStats, "energy cells", 0);
        energyCellAmount = 0;
        SetStatValue(waterStats, "water", 0);
        waterAmount = 0;
        SetStatValue(robotsStats, "robots", 0);
        robotsAmount = 0;
        SetStatValue(plantStats, "plants", 0);
        plantsAmount = 0;
        SetStatValue(seedStats, "seeds", 0);
        seedsAmount = 0;
        SetStatValue(chemicalsStats, "chemicals", 0);
        chemicalsAmount = 0;
        SetStatValue(copperWireStats, "copper wire", 0);
        copperWireAmount = 0;
        SetStatValue(metalStats, "metal", 0);
        metalAmount = 0;
        SetStatValue(deadBatteriesStats, "dead battery", 0);
        deadBatteryAmount = 0;
        SetEngineSpeed(0.0f);
        engineSpeedValue = 0.0f;
        SetTitanDistance(1200000.0f); // 1200000.0f
        distanceToTitanAmount = 1200000.0f;
        SetAirComp(0.0f, 0.0f, 0.0f, 0.0f);
        nitrogenValue = 0;
        oxygenValue = 0;
        carbonDioxdeValue = 0;
        hydrogenValue = 0;
    }

    public void ReduceRemainingDistanceToTitan()
    {
        // update the distance to titan based on current speed
        distanceToTitanAmount -= engineSpeedValue;
    }

    public void SetStatValue(TextMeshProUGUI textStatsToUpdate, string textString, float updatedValueString)
    {
        // update for values with floats (overrides previous version)
        textStatsToUpdate.text = "> " + textDictionary[textString] + ": [" + updatedValueString + "]";
    }

    public void SetAirComp(float nitrogen, float oxygen, float co2, float hydrogen)
    {
        airCompStats.text = "N: " + nitrogen + "%\nO2: " + oxygen + "%\nCo2: " + co2 + "%\nh2: " + hydrogen + "%";
    }

    public void SetEngineSpeed(float engineSpeed)
    {
        engineSpeedStats.text = "> Engine Speed: " + engineSpeed + " m/s";
    }

    public void RandomlyIncreaseDecreaseEngineSpeed()
    {
        // randomly increase or decrease speed over time
        float minSpeed = idleEngineSpeed + (idleEngineSpeed * 0.1f); // 10% slower
        float maxSpeed = idleEngineSpeed - (idleEngineSpeed * 0.1f); // 10% faster
        engineSpeedValue = (int)Random.Range(minSpeed, maxSpeed); // 10% slower, 10% faster
    }

    public void SetTitanDistance(float titanDistance)
    {
        float displayValue = Mathf.Round(titanDistance / 1000);
        if (displayValue >= 1000)
        {
            distanceToTitanStats.text = "> Distance to Titan: " + displayValue + "k km"; // add suffix for thousand
        } else
        {
            distanceToTitanStats.text = "> Distance to Titan: " + displayValue + " km";
        }
    }

    public void UpdateStaticValues(TextToDisplay textValues)
    {
        // apply new values
        energyCellAmount += textValues.energyCellCost;
        waterAmount += textValues.waterCost;
        robotsAmount += textValues.robotCost;
        plantsAmount += textValues.plantCost;
        seedsAmount += textValues.seedsCost;
        nitrogenValue += textValues.nitrogenCost;
        oxygenValue += textValues.oxygenCost;
        carbonDioxdeValue += textValues.carbonDioxdeCost;
        hydrogenValue += textValues.hydrogenCost;
        chemicalsAmount += textValues.chemicalsCost;
        copperWireAmount += textValues.copperWireCost;
        metalAmount += textValues.metalCost;
        deadBatteryAmount += textValues.deadBatteryCost;
    }

    public void UpdateEventValues(TextToDisplayEvents textValues)
    {
        // apply new values: check < 20.0 ? true : false; if lower than zero, set to zero
        // prevent values from becoming negative, otherwise set as listed
        energyCellAmount = energyCellAmount + textValues.energyCellCost < 0 ? 0 : energyCellAmount + textValues.energyCellCost;
        waterAmount = waterAmount + textValues.waterCost < 0 ? 0 : waterAmount + textValues.waterCost;
        robotsAmount = robotsAmount + textValues.robotCost < 0 ? 0 : robotsAmount + textValues.robotCost;
        plantsAmount = plantsAmount + textValues.plantCost < 0 ? 0 : plantsAmount + textValues.plantCost;
        seedsAmount = seedsAmount + textValues.seedsCost < 0 ? 0 : seedsAmount + textValues.seedsCost;
        nitrogenValue = nitrogenValue + textValues.nitrogenCost < 0 ? 0 : nitrogenValue + textValues.nitrogenCost;
        oxygenValue = oxygenValue + textValues.oxygenCost < 0 ? 0 : oxygenValue + textValues.oxygenCost;
        carbonDioxdeValue = carbonDioxdeValue + textValues.carbonDioxdeCost < 0 ? 0 : carbonDioxdeValue + textValues.carbonDioxdeCost;
        hydrogenValue = hydrogenValue + textValues.hydrogenCost < 0 ? 0 : hydrogenValue + textValues.hydrogenCost;
        chemicalsAmount = chemicalsAmount + textValues.chemicalsCost < 0 ? 0 : chemicalsAmount + textValues.chemicalsCost;
        copperWireAmount = copperWireAmount + textValues.copperWireCost < 0 ? 0 : copperWireAmount + textValues.copperWireCost;
        metalAmount = metalAmount + textValues.metalCost < 0 ? 0 : metalAmount + textValues.metalCost;
        deadBatteryAmount = deadBatteryAmount + textValues.deadBatteryCost < 0 ? 0 : deadBatteryAmount + textValues.deadBatteryCost;
    }

    public bool DoesNotHaveResources(TextToDisplay textValues)
    {
        // detect and update if any value goes below zero
        notEnoughResourcesTriggered = false;
        IsValueZeroOrNegative(energyCellAmount + textValues.energyCellCost, textDictionary["energy cells"]);
        IsValueZeroOrNegative(waterAmount + textValues.waterCost, textDictionary["water"]);
        IsValueZeroOrNegative(robotsAmount + textValues.robotCost, textDictionary["robots"]);
        IsValueZeroOrNegative(plantsAmount + textValues.plantCost, textDictionary["plants"]);
        IsValueZeroOrNegative(seedsAmount + textValues.seedsCost, textDictionary["seeds"]);
        IsValueZeroOrNegative(nitrogenValue + textValues.nitrogenCost, textDictionary["nitrogen"]);
        IsValueZeroOrNegative(oxygenValue + textValues.oxygenCost, textDictionary["oxygen"]);
        IsValueZeroOrNegative(carbonDioxdeValue + textValues.carbonDioxdeCost, textDictionary["co2"]);
        IsValueZeroOrNegative(hydrogenValue + textValues.hydrogenCost, textDictionary["hydrogen"]);
        IsValueZeroOrNegative(chemicalsAmount + textValues.chemicalsCost, textDictionary["chemicals"]);
        IsValueZeroOrNegative(copperWireAmount + textValues.copperWireCost, textDictionary["copper wire"]);
        IsValueZeroOrNegative(metalAmount + textValues.metalCost, textDictionary["metal"]);
        IsValueZeroOrNegative(deadBatteryAmount + textValues.deadBatteryCost, textDictionary["dead battery"]);

        return notEnoughResourcesTriggered;
    }

    public void IsValueZeroOrNegative(float updatedValue, string resourceName)
    {
        // prevent values from going lower than 0, if <= 0, value = 0 and triggers text
        if (updatedValue < 0)
        {
            //Debug.Log(resourceName + " is '" + updatedValue + "'  VALUE IS ZERO OR BELOW");
            if (!notEnoughResourcesTriggered)
            {
                terminalTextManager.startWriteText("Not enough " + resourceName);
            }
            notEnoughResourcesTriggered = true;
        }
    }

    public IEnumerator SetEngineSpeed(bool engineIsOn)
    {
        // speed of ship when engine is on ( 3 km/s)
        float buildUpTime = gameManager.engineCooldown; // build up to target speed over engine cooldown time

        float elapsedTime = 0.0f;
        // if engine is on: build up
        if (engineIsOn)
        {
            Debug.Log("slowly build up speed to max speed");
            while (elapsedTime < buildUpTime)
            {
                elapsedTime += Time.deltaTime;
                engineSpeedValue = (int)Mathf.Lerp(0, idleEngineSpeed, elapsedTime / buildUpTime);
                yield return null;
            }
        } else // if engine is off: slow downs
        {
            Debug.Log("slowly slow down speed to stop");
            float currentEngineSpeed = engineSpeedValue;
            while (elapsedTime < buildUpTime)
            {
                elapsedTime += Time.deltaTime;
                engineSpeedValue = (int)Mathf.Lerp(currentEngineSpeed, 0, elapsedTime / buildUpTime);
                yield return null;
            }
        }

    }
}