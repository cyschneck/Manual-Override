using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipOnHover : MonoBehaviour
{
    // Controls the popup Tooltip for interactable buttons

    [Header("References")]
    private GameManager gameManager;
    private GameObject toolTipOnDisplay;

    [Header("Display Textboxes")]
    private GameObject energyCellTooltip;
    private GameObject waterTooltip;
    private GameObject nitrogenTooltip;
    private GameObject oxygenTooltip;
    private GameObject carbonDioxdTooltip;
    private GameObject hydrogenTooltip;
    private GameObject seedsTooltip;
    private GameObject plantsTooltip;
    private GameObject methaneTooltip;
    private GameObject chemicalsTooltip;
    private GameObject robotsTooltip;
    private GameObject copperWireTooltip;
    private GameObject metalTooltip;
    private GameObject batteryTooltip;

    private bool isTooltipNotEmpty;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Debug.Log("this gameobject: " + this.gameObject.name);
        toolTipOnDisplay = this.gameObject.transform.GetChild(1).gameObject; // tooltip is the second child

        // set tooltip options based on current button (based on the postion of the tooltip in the tooltip)
        energyCellTooltip = toolTipOnDisplay.transform.GetChild(0).gameObject;
        waterTooltip = toolTipOnDisplay.transform.GetChild(1).gameObject;
        nitrogenTooltip = toolTipOnDisplay.transform.GetChild(2).gameObject;
        oxygenTooltip = toolTipOnDisplay.transform.GetChild(3).gameObject;
        carbonDioxdTooltip = toolTipOnDisplay.transform.GetChild(4).gameObject;
        hydrogenTooltip = toolTipOnDisplay.transform.GetChild(5).gameObject;
        seedsTooltip = toolTipOnDisplay.transform.GetChild(6).gameObject;
        plantsTooltip = toolTipOnDisplay.transform.GetChild(7).gameObject;
        methaneTooltip = toolTipOnDisplay.transform.GetChild(8).gameObject;
        chemicalsTooltip = toolTipOnDisplay.transform.GetChild(9).gameObject;
        robotsTooltip = toolTipOnDisplay.transform.GetChild(10).gameObject;
        copperWireTooltip = toolTipOnDisplay.transform.GetChild(11).gameObject;
        metalTooltip = toolTipOnDisplay.transform.GetChild(12).gameObject;
        batteryTooltip = toolTipOnDisplay.transform.GetChild(13).gameObject;
    }

    private void Update()
    {
        // populate tooltip based on scriptable object based on which button is being hovered on
        if (this.gameObject == gameManager.germinateSeedsButton) { SetValuesFromScriptableObject(gameManager.germinateSeedsText); }
        if (this.gameObject == gameManager.plantSeedsButton) { SetValuesFromScriptableObject(gameManager.plantSeedsText); }
        if (this.gameObject == gameManager.treatPlantsButton) { SetValuesFromScriptableObject(gameManager.treatPlantsText); }
        if (this.gameObject == gameManager.performElectrolysisButton) { SetValuesFromScriptableObject(gameManager.performElectrolysisText); }
        if (this.gameObject == gameManager.performSabatierButton) { SetValuesFromScriptableObject(gameManager.performSabatierText); }
        if (this.gameObject == gameManager.stopEngineButton) { SetValuesFromScriptableObject(gameManager.stopEngineText); }
        if (this.gameObject == gameManager.assembleRobotsButton) { SetValuesFromScriptableObject(gameManager.assembleRobotsText); }
        if (this.gameObject == gameManager.dismantleRobotsButton) { SetValuesFromScriptableObject(gameManager.dismantleRobotsText); }
        if (this.gameObject == gameManager.scanButton) { SetValuesFromScriptableObject(gameManager.startScanText); }
        if (this.gameObject == gameManager.deployMiningRobotsButton) { SetValuesFromScriptableObject(gameManager.deployMiningRobotsText); }
        if (this.gameObject == gameManager.assembleBatteryButton) { SetValuesFromScriptableObject(gameManager.assembleBatteryText); }
        if (this.gameObject == gameManager.dismantleBatteryButton) { SetValuesFromScriptableObject(gameManager.dismantleRobotsText); }
        if (this.gameObject == gameManager.continueOptionButton) { SetValuesFromScriptableObject(gameManager.continueOptionText); }
        if (this.gameObject == gameManager.yesOptionButton) { SetValuesFromScriptableObject(gameManager.yesOptionText); }
        if (this.gameObject == gameManager.noOptionButton) { SetValuesFromScriptableObject(gameManager.noOptionText); }
    }

    public void SetValuesFromScriptableObject(TextToDisplay displayText)
    {
        // set the values for the tooltip from the JSON
        SetTooltip(energyCellTooltip, displayText.energyCellCost);
        SetTooltip(waterTooltip, displayText.waterCost);
        SetTooltip(nitrogenTooltip, displayText.nitrogenCost);
        SetTooltip(oxygenTooltip, displayText.oxygenCost);
        SetTooltip(carbonDioxdTooltip, displayText.carbonDioxdeCost);
        SetTooltip(hydrogenTooltip, displayText.hydrogenCost);
        SetTooltip(seedsTooltip, displayText.seedsCost);
        SetTooltip(plantsTooltip, displayText.plantsCost);
        SetTooltip(methaneTooltip, displayText.methaneCost);
        SetTooltip(chemicalsTooltip, displayText.chemicalsCost);
        SetTooltip(robotsTooltip, displayText.robotCost);
        SetTooltip(copperWireTooltip, displayText.copperWireCost);
        SetTooltip(metalTooltip, displayText.metalCost);
        SetTooltip(batteryTooltip, displayText.deadBatteryCost);

        // if tooltip has no values, do not display tooltip
        if (displayText.energyCellCost == 0 && displayText.waterCost == 0 && displayText.nitrogenCost == 0 && displayText.oxygenCost == 0
            && displayText.carbonDioxdeCost == 0 && displayText.hydrogenCost == 0 && displayText.seedsCost == 0 && displayText.plantsCost == 0
            && displayText.methaneCost == 0 && displayText.chemicalsCost == 0 && displayText.robotCost == 0 && displayText.copperWireCost == 0
            && displayText.metalCost == 0 && displayText.deadBatteryCost == 0)
        {
            isTooltipNotEmpty = false;
        } else
        {
            isTooltipNotEmpty = true;
        }
    }


    private void SetTooltip(GameObject tooltip, float tooltipValue)
    {
        //Debug.Log(tooltip.name + ": " + tooltipValue);
        if (tooltipValue == 0)
        {
            // do not display option if it has no value associated with it
            tooltip.SetActive(false);
        } else
        {
            tooltip.SetActive(true);
            tooltip.GetComponent<TextMeshProUGUI>().text = tooltip.name + ": " + tooltipValue.ToString();
        }

    }

    public void HoverOver()
    {
        //Debug.Log("Mouse hover over: " + this.gameObject.name);
        //Debug.Log("Mouse hover over: " + toolTipOnDisplay.name);
        if (isTooltipNotEmpty)
        {
            toolTipOnDisplay.SetActive(true);
        }
    }

    public void LeftHover()
    {
        toolTipOnDisplay.SetActive(false);
    }

    public void ResetDefaultValues(TextToDisplay existingValue)
    {
        // set default values for continue/yes/no to save between popups
        existingValue.energyCellCost = 123;
        existingValue.waterCost = 123;
        existingValue.nitrogenCost = 123;
        existingValue.oxygenCost = 123;
        existingValue.carbonDioxdeCost = 123;
        existingValue.hydrogenCost = 123;
        existingValue.seedsCost = 123;
        existingValue.plantsCost = 123;
        existingValue.methaneCost = 123;
        existingValue.chemicalsCost = 123;
        existingValue.robotCost = 123;
        existingValue.copperWireCost = 123;
        existingValue.metalCost = 123;
        existingValue.deadBatteryCost = 123;
    }
}
