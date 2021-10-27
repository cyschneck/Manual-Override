using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    // Stats Manager controls updating stats on terminal (non-strings, numerical values)

    [Header("Reference")]
    private GameManager gameManager;
    private CooldownBar cooldownBar;
    private StatsForStringValues stringStatsValues;
    private TerminalTextManager terminalTextManager;

    [Header("Static Stats")]
    public float energyCellAmount;
    public float waterAmount;
    public float shipMassValue;
    public float rocketThurstValue;
    public float shipSpeedValue;
    public float distanceToTitanValue;
    public float nitrogenValue;
    public float oxygenValue;
    public float carbonDioxdeValue;
    public float hydrogenValue;
    public float seedsAmount;
    public float plantsAmount;
    public float methaneAmount;
    public float chemicalsAmount;
    public float robotsAmount;
    public float copperWireAmount;
    public float metalAmount;
    public float deadBatteryAmount;
    public float distanceToTitanAmount;
    public float distanceChangeDelay;
    public float timeBetweenDelay; // how often to update distance
    private bool notEnoughResourcesTriggered; // trigger a text value if any values in the list are zero
    private int idleCruisingRocketThrust = 26000; // kph
    public float titanTotalDistance = 1277000000.0f; // distance to Titan from Earth (km)
    public float accelerationOfShip;

    [Header("Ship Main Stats")]
    public TextMeshProUGUI energyCellStats;
    public TextMeshProUGUI waterStats;
    public TextMeshProUGUI shipMassStats;
    public TextMeshProUGUI rocketThrustStats;
    public TextMeshProUGUI shipSpeedStats;
    public TextMeshProUGUI distanceToTitanStats;
    public TextMeshProUGUI airCompStats;

    [Header("Ship Science Stats")]
    public TextMeshProUGUI plantStats;
    public TextMeshProUGUI seedStats;
    public TextMeshProUGUI methaneStats;
    public TextMeshProUGUI chemicalsStats;

    [Header("Ship Engineering Stats")]
    public TextMeshProUGUI robotsStats;
    public TextMeshProUGUI copperWireStats;
    public TextMeshProUGUI metalStats;
    public TextMeshProUGUI deadBatteriesStats;

    private Dictionary<string, string> textDictionary = new Dictionary<string, string>();
    private bool isEngineChangingSpeed;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        cooldownBar = GameObject.Find("CooldownManager").GetComponent<CooldownBar>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();
        stringStatsValues = GameObject.Find("StatsManager").GetComponent<StatsForStringValues>();

        // dictionary to store displayed version of stats name (chemical = "avaiable chemicals")
        textDictionary.Add("energy cells", "Energy Cells");
        textDictionary.Add("water", "water");
        textDictionary.Add("robots", "Robotics");
        textDictionary.Add("plants", "Plants");
        textDictionary.Add("seeds", "Seeds");
        textDictionary.Add("methane", "Methane");
        textDictionary.Add("chemicals", "Avaliable Chemicals");
        textDictionary.Add("nitrogen", "Nitrogen Gas");
        textDictionary.Add("oxygen", "Oxygen Gas");
        textDictionary.Add("co2", "Carbon Dioxide Gas");
        textDictionary.Add("hydrogen", "Hydrogen Gas");
        textDictionary.Add("copper wire", "Copper Wire");
        textDictionary.Add("metal", "Processed Metal");
        textDictionary.Add("dead battery", "Dead Batteries");

        // how often to update distance to titan
        distanceChangeDelay = 10.0f; // update every x seconds
        timeBetweenDelay = 0.0f;
    }

    private void FixedUpdate()
    {
        // update values as they are updated
        SetStatValue(energyCellStats, "energy cells", energyCellAmount);
        SetStatValue(waterStats, "water", waterAmount);
        SetStatValue(robotsStats, "robots", robotsAmount);
        SetStatValue(plantStats, "plants", plantsAmount);
        SetStatValue(seedStats, "seeds", seedsAmount);
        SetStatValue(methaneStats, "methane", methaneAmount);
        SetStatValue(chemicalsStats, "chemicals", chemicalsAmount);
        SetStatValue(copperWireStats, "copper wire", copperWireAmount);
        SetStatValue(metalStats, "metal", metalAmount);
        SetStatValue(deadBatteriesStats, "dead battery", deadBatteryAmount);
        SetRocketThrust(rocketThurstValue);
        UpdateShipSpeedBasedOnRocketThrust(rocketThurstValue);
        SetShipMass(shipMassValue);
        SetShipSpeed(shipSpeedValue);
        SetTitanDistance(distanceToTitanAmount);
        SetAirComp(nitrogenValue, oxygenValue, carbonDioxdeValue, hydrogenValue);

        // calculate the distance every second while the engine is on, randomly change speed by 10%
        if (gameManager.isEngineOn && !gameManager.gameIsPaused)
        {
            timeBetweenDelay += 1.0f * Time.deltaTime;
            if (timeBetweenDelay >= distanceChangeDelay)
            {
                // update every x seconds
                if (!isEngineChangingSpeed) // only change speed when not currently building up/losing speed (changes randomly during idle)
                {
                    ReduceRemainingDistanceToTitan();
                    RandomlyIncreaseDecreaseRocketThurst();
                    timeBetweenDelay = 0.0f;
                }
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
        energyCellAmount = 0;
        SetStatValue(energyCellStats, "energy cells", energyCellAmount);
        waterAmount = 0;
        SetStatValue(waterStats, "water", waterAmount);
        robotsAmount = 0;
        SetStatValue(robotsStats, "robots", robotsAmount);
        plantsAmount = 0;
        SetStatValue(plantStats, "plants", plantsAmount);
        seedsAmount = 0;
        SetStatValue(seedStats, "seeds", seedsAmount);
        methaneAmount = 0;
        SetStatValue(methaneStats, "methane", methaneAmount);
        chemicalsAmount = 0;
        SetStatValue(chemicalsStats, "chemicals", chemicalsAmount);
        copperWireAmount = 0;
        SetStatValue(copperWireStats, "copper wire", copperWireAmount);
        metalAmount = 0;
        SetStatValue(metalStats, "metal", metalAmount);
        deadBatteryAmount = 0;
        SetStatValue(deadBatteriesStats, "dead battery", deadBatteryAmount);
        shipMassValue = 420.0f; // 420,000 kg (ISS) = 420 Mg
        SetShipMass(shipMassValue);
        rocketThurstValue = 0.0f;
        SetRocketThrust(rocketThurstValue);
        shipSpeedValue = 0.0f;
        SetShipSpeed(shipSpeedValue);
        distanceToTitanAmount = titanTotalDistance;
        SetTitanDistance(distanceToTitanAmount); // 1200000.0f
        nitrogenValue = 55.0f;
        oxygenValue = 22.0f;
        carbonDioxdeValue = 3.0f;
        hydrogenValue = 2.0f;
        SetAirComp(nitrogenValue, oxygenValue, carbonDioxdeValue, hydrogenValue);
    }

    public void ReduceRemainingDistanceToTitan()
    {
        // update the distance to titan based on current speed
        distanceToTitanAmount -= rocketThurstValue;
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

    public void SetRocketThrust(float rocketThrustForce)
    {
        rocketThrustStats.text = "> Rocket Thrust: " + rocketThrustForce + " n";
    }

    public void SetShipMass(float shipMass)
    {
        shipMassStats.text = "> Ship Mass: " + shipMass + " mg";
    }

    public void SetShipSpeed(float shipSpeed)
    {
        // set up ship speed that is impacted by the non-fiction enviorment and gravity wells
        shipSpeedStats.text = "> Ship Speed: " + shipSpeed + " k/h";
    }
    public void RandomlyIncreaseDecreaseRocketThurst()
    {
        // randomly increase or decrease speed over time
        float minThrust = idleCruisingRocketThrust + (idleCruisingRocketThrust * 0.05f); // 5% slower
        float maxThrust = idleCruisingRocketThrust - (idleCruisingRocketThrust * 0.05f); // 5% faster
        rocketThurstValue = (int)Random.Range(minThrust, maxThrust); // 5% slower, 5% faster
    }

    public void SetTitanDistance(float titanDistance)
    {
        float displayValue = Mathf.Round(titanDistance / 1000000); // divided by a million
        if (displayValue >= 1000)
        {
            distanceToTitanStats.text = "> Distance to Titan: " + displayValue + "mk km"; // add suffix for million and thousand
        }
        if (displayValue < 1000 && displayValue >= 100)
        {
            distanceToTitanStats.text = "> Distance to Titan: " + displayValue + "k km"; // remove M suffix
        }
        if (displayValue < 100)
        {
            distanceToTitanStats.text = "> Distance to Titan: " + displayValue + " km"; // remove K suffix
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
        methaneAmount += textValues.methaneCost;
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
        methaneAmount = methaneAmount + textValues.methaneCost < 0 ? 0 : methaneAmount + textValues.methaneCost;
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
        IsValueZeroOrNegative(methaneAmount + textValues.methaneCost, textDictionary["methane"]);
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
            // resource value is zero or below zero
            if (!notEnoughResourcesTriggered)
            {
                terminalTextManager.startWriteText("Not enough " + resourceName);
            }
            notEnoughResourcesTriggered = true;
        }
    }

    public IEnumerator SetRocketThrust(bool engineIsOn)
    {
        isEngineChangingSpeed = false;

        // speed of ship when engine is on ( x km/s)
        float buildUpTime = cooldownBar.engineCooldown; // build up to target speed over engine cooldown time

        float elapsedTime = 0.0f;
        // if engine is on: build up
        if (engineIsOn)
        {
            //slowly build up speed to max speed
            while (elapsedTime < buildUpTime)
            {
                isEngineChangingSpeed = true;
                elapsedTime += Time.deltaTime;
                rocketThurstValue = (int)Mathf.Lerp(0, idleCruisingRocketThrust, elapsedTime / buildUpTime);
                yield return null;
            }
        } else // if engine is off: slow downs
        {
            //slowly slow down speed to stop
            float currentEngineSpeed = rocketThurstValue;
            while (elapsedTime < buildUpTime)
            {
                isEngineChangingSpeed = true;
                elapsedTime += Time.deltaTime;
                rocketThurstValue = (int)Mathf.Lerp(currentEngineSpeed, 0, elapsedTime / buildUpTime);
                yield return null;
            }
        }
        isEngineChangingSpeed = false; // engine in the process of gaining/losing speed
    }

    public void UpdateShipSpeedBasedOnRocketThrust(float rocketThrustValue)
    {
        // engine speed based on engine on/off and direction
        // thrust = velocity * (change of mass/change in time)
        accelerationOfShip = rocketThrustValue / shipMassValue; // f = ma where a = f/m
        shipSpeedValue += (int)(accelerationOfShip * Time.deltaTime);
    }
}