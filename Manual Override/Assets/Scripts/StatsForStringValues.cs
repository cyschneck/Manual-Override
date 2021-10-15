using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum StatsStringsOptions { offline, nominal, poor }

public class StatsForStringValues : MonoBehaviour
{
    // updates string values from: [offline, nominal, poor]
    // currently applied for: heat, soil health, plant health

    [Header("Reference")]
    private GameManager gameManager;
    private TerminalTextManager terminalTextManager;


    [Header("String Stats")]
    public TextMeshProUGUI heatStats;
    public TextMeshProUGUI soilHealthStats;
    public TextMeshProUGUI plantHealthStats;
    private float heatRangeBackground;
    private float soilHealthBackground;
    private float plantHealthBackground;
    private string heatValue;
    private string soilHealthValue;
    private string plantHealthValue;
    private int offlineHighRange = 5;
    private int poorHighRange = 20;
    private int nominalHighRange = 100;
    private float heatGainLoss = 0.015f; // gains per fixed update

    [Header("Terminal Text")]
    public TextToDisplay heatOffline;
    public TextToDisplay heatPoor;
    public TextToDisplay heatNominal;
    public TextToDisplay soilHealthOffline;
    public TextToDisplay soilHealthPoor;
    public TextToDisplay soilHealthNominal;
    public TextToDisplay plantHealthOffline;
    public TextToDisplay plantHealthPoor;
    public TextToDisplay plantHealthNominal;

    private Dictionary<string, string> stringValueDictionary = new Dictionary<string, string>();
    private Dictionary<StatsStringsOptions, string> stringDisplayStringDictionary = new Dictionary<StatsStringsOptions, string>();


    private void Awake()
    {
        // range of values: 0 - 100, where offline < 5, poor is 6-20, nominal is 21-100

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();

        // dictionary to store displayed version of stats name
        stringValueDictionary.Add("heat", "Heat");
        stringValueDictionary.Add("soil health", "Soil Health");
        stringValueDictionary.Add("plant health", "Plant Health");

        // dictionary to store displayed options
        stringDisplayStringDictionary.Add(StatsStringsOptions.offline, "offline");
        stringDisplayStringDictionary.Add(StatsStringsOptions.nominal, "nominal");
        stringDisplayStringDictionary.Add(StatsStringsOptions.poor, "poor");

        SetDefaultValuesStrings();
    }

    private void Update()
    {
        heatValue = SetBackgroundValue(heatRangeBackground);
        SetStringStatValue(heatStats, "heat", heatValue);

        soilHealthValue = SetBackgroundValue(soilHealthBackground);
        SetStringStatValue(soilHealthStats, "soil health", soilHealthValue);

        plantHealthValue = SetBackgroundValue(plantHealthBackground);
        SetStringStatValue(plantHealthStats, "plant health", plantHealthValue);
    }

    private void FixedUpdate()
    {
        // set up values in fixed time
        if (!gameManager.gameIsPaused) { SetHeatBasedOnEngine(gameManager.isEngineOn); } // while engine is on, increment heat, if off, loss heat
    }

    public void SetHeatBasedOnEngine(bool engineIsOn)
    {
        // set the heat to grow or fall when the engine is on
        if (engineIsOn) { UpdateBackgroundValue(heatGainLoss, "heat"); } // heat gain
        if (!engineIsOn) { UpdateBackgroundValue(-heatGainLoss, "heat"); } // heat loss
    }

    public void SetDefaultValuesStrings()
    {
        // set string value based on background value (offline at beginning)
        heatRangeBackground = 0;
        heatValue = SetBackgroundValue(heatRangeBackground);
        SetStringStatValue(heatStats, "heat", heatValue);

        soilHealthBackground = 0;
        soilHealthValue = SetBackgroundValue(soilHealthBackground);
        SetStringStatValue(soilHealthStats, "soil health", soilHealthValue);

        plantHealthBackground = 0;
        plantHealthValue = SetBackgroundValue(plantHealthBackground);
        SetStringStatValue(plantHealthStats, "plant health", plantHealthValue);
    }

    private string SetBackgroundValue(float valueUpdated)
    {
        // update the background string value
        if (0 <= valueUpdated && valueUpdated <= offlineHighRange) // offline (0-5)
        {
            return stringDisplayStringDictionary[StatsStringsOptions.offline];
        }
        if (offlineHighRange < valueUpdated && valueUpdated <= poorHighRange) // poor (6-20)
        {
            return stringDisplayStringDictionary[StatsStringsOptions.poor];
        }
        if (poorHighRange < valueUpdated && valueUpdated <= nominalHighRange) // nominal (21-100)
        {
            return stringDisplayStringDictionary[StatsStringsOptions.nominal];
        }
        return "error";
    }

    public void UpdateBackgroundValue(float updatedValue, string textString)
    {
        // update background values
        if (textString == "heat")
        {
            if (heatRangeBackground + updatedValue > 100) 
            {
                CheckIfEnteredNewRange(heatRangeBackground, 100, "heat");
                heatRangeBackground = 100; 
            }
            if (heatRangeBackground + updatedValue < 0) 
            {
                CheckIfEnteredNewRange(heatRangeBackground, 0, "heat");
                heatRangeBackground = 0; 
            }
            if (0 < heatRangeBackground + updatedValue && heatRangeBackground + updatedValue < 100) 
            {
                CheckIfEnteredNewRange(heatRangeBackground, heatRangeBackground + updatedValue, "heat");
                heatRangeBackground += updatedValue; 
            }
        }

        if (textString == "soil health")
        {
            if (soilHealthBackground + updatedValue > 100) 
            {
                CheckIfEnteredNewRange(soilHealthBackground, 100, "soil health");
                soilHealthBackground = 100; 
            }
            if (soilHealthBackground + updatedValue < 0) 
            {
                CheckIfEnteredNewRange(soilHealthBackground, 0, "soil health");
                soilHealthBackground = 0; 
            }
            if (0 < soilHealthBackground + updatedValue && soilHealthBackground + updatedValue < 100) 
            {
                CheckIfEnteredNewRange(soilHealthBackground, soilHealthBackground + updatedValue, "soil health");
                soilHealthBackground += updatedValue; 
            }
        }

        if (textString == "plant health")
        {
            if (plantHealthBackground + updatedValue > 100) 
            {
                CheckIfEnteredNewRange(plantHealthBackground, 100, "plant health");
                plantHealthBackground = 100; 
            }
            if (plantHealthBackground + updatedValue < 0) 
            {
                CheckIfEnteredNewRange(plantHealthBackground, 0, "plant health");
                plantHealthBackground = 0; 
            }
            if (0 < plantHealthBackground + updatedValue && plantHealthBackground + updatedValue < 100) 
            {
                CheckIfEnteredNewRange(plantHealthBackground, plantHealthBackground + updatedValue, "plant health");
                plantHealthBackground += updatedValue; 
            }
        }
    }

    private void CheckIfEnteredNewRange(float currentValue, float newValue, string textString)
    {
        // check if range for a value moved into a new range

        // from offline to poor
        if (0 <= currentValue && currentValue <= offlineHighRange)  // offline
        {
            if (offlineHighRange < newValue && newValue <= poorHighRange) // poor
            {
                //Debug.Log("triggering: poor (o-p)"); // from small number to large number
                if (textString == "heat") { terminalTextManager.startWriteText(heatPoor.text); }
                if (textString == "soil health") { terminalTextManager.startWriteText(soilHealthPoor.text); }
                if (textString == "plant health") { terminalTextManager.startWriteText(plantHealthPoor.text); }
                
            }
        }

        // from poor to nominal
        if (offlineHighRange < currentValue && currentValue <= poorHighRange) // poor
        {
            if (poorHighRange < newValue && newValue <= nominalHighRange)  // nominal
            {
                //Debug.Log("triggering: nominal (p-n)"); // from small number to lalrge number
                if (textString == "heat") { terminalTextManager.startWriteText(heatNominal.text); }
                if (textString == "soil health") { terminalTextManager.startWriteText(soilHealthNominal.text); }
                if (textString == "plant health") { terminalTextManager.startWriteText(plantHealthNominal.text); }

            }
        }

        // from nominal to poor
        if (poorHighRange < currentValue && currentValue <= nominalHighRange) // nominal
        {
            if (offlineHighRange < newValue && newValue <= poorHighRange) // poor
            {
                //Debug.Log("triggering: poor (n-p)"); // from large number to small number
                if (textString == "heat") { terminalTextManager.startWriteText(heatPoor.text); }
                if (textString == "soil health") { terminalTextManager.startWriteText(soilHealthPoor.text); }
                if (textString == "plant health") { terminalTextManager.startWriteText(plantHealthPoor.text); }

            }
        }

        // from poor to offline
        if (offlineHighRange < currentValue && currentValue <= poorHighRange) // poor
        {
            if (0 <= newValue && newValue <= offlineHighRange) // offline
            {
                //Debug.Log("triggering: offline (p-o)"); // from large number to small number
                if (textString == "heat") { terminalTextManager.startWriteText(heatOffline.text); }
                if (textString == "soil health") { terminalTextManager.startWriteText(soilHealthOffline.text); }
                if (textString == "plant health") { terminalTextManager.startWriteText(plantHealthOffline.text); }

            }
        }
    }

    public void SetStringStatValue(TextMeshProUGUI textStatsToUpdate, string textString, string updatedValueString)
    {
        // update for values that store strings: [nominal]
        textStatsToUpdate.text = "> " + stringValueDictionary[textString] + ": [" + updatedValueString + "]";
    }

}
