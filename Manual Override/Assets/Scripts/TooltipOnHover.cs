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
    private GameObject robotsTooltip;
    private GameObject plantsTooltip;
    private GameObject seedsTooltip;
    private GameObject methaneTooltip;
    private GameObject nitrogenTooltip;
    private GameObject oxygenTooltip;
    private GameObject carbonDioxdTooltip;
    private GameObject hydrogenTooltip;
    private GameObject chemicalsTooltip;
    private GameObject copperWireTooltip;
    private GameObject metalTooltip;
    private GameObject batteryTooltip;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Debug.Log("this gameobject: " + this.gameObject.name);
        toolTipOnDisplay = this.gameObject.transform.GetChild(2).gameObject; // tooltip is the second child

        // set tooltip options based on current button (based on the postion of the tooltip in the tooltip)
        energyCellTooltip = toolTipOnDisplay.transform.GetChild(0).gameObject;
        waterTooltip = toolTipOnDisplay.transform.GetChild(1).gameObject;
        robotsTooltip = toolTipOnDisplay.transform.GetChild(2).gameObject;
        plantsTooltip = toolTipOnDisplay.transform.GetChild(3).gameObject;
        seedsTooltip = toolTipOnDisplay.transform.GetChild(4).gameObject;
        methaneTooltip = toolTipOnDisplay.transform.GetChild(5).gameObject;
        nitrogenTooltip = toolTipOnDisplay.transform.GetChild(6).gameObject;
        oxygenTooltip = toolTipOnDisplay.transform.GetChild(7).gameObject;
        carbonDioxdTooltip = toolTipOnDisplay.transform.GetChild(8).gameObject;
        hydrogenTooltip = toolTipOnDisplay.transform.GetChild(9).gameObject;
        chemicalsTooltip = toolTipOnDisplay.transform.GetChild(10).gameObject;
        copperWireTooltip = toolTipOnDisplay.transform.GetChild(11).gameObject;
        metalTooltip = toolTipOnDisplay.transform.GetChild(12).gameObject;
        batteryTooltip = toolTipOnDisplay.transform.GetChild(13).gameObject;

        // populate tooltip based on scriptable object based on which button is being hovered on
        if (this.gameObject == gameManager.germinateSeedsButton) { SetValuesFromScriptableObject(gameManager.germinateSeedsText);}
        if (this.gameObject == gameManager.plantSeedsButton) { SetValuesFromScriptableObject(gameManager.plantSeedsText);}
        if (this.gameObject == gameManager.treatPlantsButton) { SetValuesFromScriptableObject(gameManager.treatPlantsText);}
        if (this.gameObject == gameManager.performElectrolysisButton) { SetValuesFromScriptableObject(gameManager.performElectrolysisText);}
        if (this.gameObject == gameManager.performSabatierButton) { SetValuesFromScriptableObject(gameManager.performSabatierText);}
        if (this.gameObject == gameManager.assembleRobotsButton) { SetValuesFromScriptableObject(gameManager.assembleBatteryText);}
        if (this.gameObject == gameManager.dismantleRobotsButton) { SetValuesFromScriptableObject(gameManager.dismantleRobotsText);}
        if (this.gameObject == gameManager.scanButton) { SetValuesFromScriptableObject(gameManager.startScanText);}
        if (this.gameObject == gameManager.deployMiningRobotsButton) { SetValuesFromScriptableObject(gameManager.deployMiningRobotsText);}
        if (this.gameObject == gameManager.assembleBatteryButton) { SetValuesFromScriptableObject(gameManager.assembleBatteryText);}
        if (this.gameObject == gameManager.dismantleBatteryButton) { SetValuesFromScriptableObject(gameManager.dismantleRobotsText);}
    }

    private void SetValuesFromScriptableObject(TextToDisplay displayText)
    {
        // set the values for the tooltip from the JSON
        SetTooltip(energyCellTooltip, displayText.energyCellCost);
        SetTooltip(waterTooltip, displayText.waterCost);
        SetTooltip(robotsTooltip, displayText.robotCost);
        SetTooltip(plantsTooltip, displayText.plantCost);
        SetTooltip(seedsTooltip, displayText.seedsCost);
        SetTooltip(methaneTooltip, displayText.methaneCost);
        SetTooltip(nitrogenTooltip, displayText.nitrogenCost);
        SetTooltip(oxygenTooltip, displayText.oxygenCost);
        SetTooltip(carbonDioxdTooltip, displayText.carbonDioxdeCost);
        SetTooltip(hydrogenTooltip, displayText.hydrogenCost);
        SetTooltip(chemicalsTooltip, displayText.chemicalsCost);
        SetTooltip(copperWireTooltip, displayText.copperWireCost);
        SetTooltip(metalTooltip, displayText.metalCost);
        SetTooltip(batteryTooltip, displayText.deadBatteryCost);
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
            tooltip.GetComponent<TextMeshProUGUI>().text = tooltip.name + ": " + tooltipValue.ToString();
        }

    }

    public void HoverOver()
    {
        //Debug.Log("Mouse hover over: " + this.gameObject.name);
        //Debug.Log("Mouse hover over: " + toolTip.name);
        toolTipOnDisplay.SetActive(true);
    }

    public void LeftHover()
    {
        toolTipOnDisplay.SetActive(false);
    }
}
