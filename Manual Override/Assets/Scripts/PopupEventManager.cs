﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PopupEventManager : MonoBehaviour
{
    // Controls Menus and Popups UI generated by events

    [Header("Reference")]
    private GameManager gameManager;
    private StatsManager statsManager;
    private TerminalTextManager terminalTextManager;
    private TooltipOnHover tooltipHoverContinue;
    private TooltipOnHover tooltipHoverYes;
    private TooltipOnHover tooltipHoverNo;

    [Header("Popup Menu and Options")]
    public GameObject popUpMenu;
    public GameObject eventTextBox;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;
    public EventManager eventManager;
    public bool isPopUpActive = false;
    private TextToDisplayEvents eventObject;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        terminalTextManager = GameObject.Find("TerminalTextManager").GetComponent<TerminalTextManager>();
        tooltipHoverContinue = continueButton.GetComponent<TooltipOnHover>();
        tooltipHoverYes = yesButton.GetComponent<TooltipOnHover>();
        tooltipHoverNo = noButton.GetComponent<TooltipOnHover>();
    }

    private void FixedUpdate()
    {
        if (isPopUpActive)
        {
            // pause game when popup is active
            gameManager.StopTimePause();
            gameManager.DisableOrEnableAllTermianlButtonsInteractable(false); // make buttons not interactable
        }
    }

    public void SetUpPopup(TextToDisplayEvents displayValue)
    {
        eventObject = displayValue;
        // set up values for each option continue/yes/no
        eventTextBox.GetComponent<TextMeshProUGUI>().text = eventObject.eventText;
        if (eventObject.popUpMenuOption == popUpMenuOptions.continueOnly)
        {
            SetUpContinueYesNoTooltip(gameManager.continueOptionText);
            SetUpGenericEvent();
        }
        else
        {
            SetUpContinueYesNoTooltip(gameManager.yesOptionText);
            SetUpContinueYesNoTooltip(gameManager.noOptionText);
            SetUpYesAndNoEvent();
        }
        popUpMenu.SetActive(true);
        isPopUpActive = true;
    }

    public void SetUpContinueYesNoTooltip(TextToDisplay existingValue)
    {
        // set up the dynamic tooltip for popup events (based on which event is being displayed)
        existingValue.hydrogenCellCost = eventObject.hydrogenCellCost;
        existingValue.waterCost = eventObject.waterCost;
        existingValue.nitrogenCost = eventObject.nitrogenCost;
        existingValue.oxygenCost = eventObject.oxygenCost;
        existingValue.carbonDioxdeCost = eventObject.carbonDioxdeCost;
        existingValue.hydrogenCost = eventObject.hydrogenCost;
        existingValue.seedsCost = eventObject.seedsCost;
        existingValue.plantsCost = eventObject.plantsCost;
        existingValue.methaneCost = eventObject.methaneCost;
        existingValue.chemicalsCost = eventObject.chemicalsCost;
        existingValue.robotCost = eventObject.robotCost;
        existingValue.copperWireCost = eventObject.copperWireCost;
        existingValue.metalCost = eventObject.metalCost;

    }
    private void SetUpYesAndNoEvent()
    {
        // set up menu with yes and no options
        continueButton.SetActive(false);
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    private void SetUpGenericEvent()
    {
        // set up menu with just a continue button
        continueButton.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
    public void ContinueButton()
    {
        Debug.Log("PRESSED CONTINUE");
        tooltipHoverContinue.LeftHover(); // close popup tooltip
        tooltipHoverContinue.ResetDefaultValues(gameManager.continueOptionText);
        StartCoroutine(ClosePopUpMenu());
    }

    public void YesButton()
    {
        Debug.Log("PRESSED YES");
        tooltipHoverYes.LeftHover(); // close popup tooltip
        tooltipHoverYes.ResetDefaultValues(gameManager.yesOptionText);
        StartCoroutine(ClosePopUpMenu());
    }

    public void NoButton()
    {
        Debug.Log("PRESSED NO");
        tooltipHoverNo.LeftHover(); // close popup tooltip
        tooltipHoverNo.ResetDefaultValues(gameManager.noOptionText);
        StartCoroutine(ClosePopUpMenu());
    }

    public IEnumerator ClosePopUpMenu()
    {
        // close popup menu, restart time, apply event stats, display terminal text
        gameManager.StartGameFromPause(); // restarts time
        gameManager.DisableOrEnableAllTermianlButtonsInteractable(true); // make buttons not interactable

        popUpMenu.SetActive(false);
        isPopUpActive = false;

        yield return null;

        TriggerEventTextWithStatsUpdate(eventObject);
    }

    public void TriggerEventTextWithStatsUpdate(TextToDisplayEvents displayValue)
    {
        // display the termainl text and update 
        terminalTextManager.startWriteText(displayValue.eventTerminalText); // write a summary to terminal for records
        statsManager.UpdateEventValues(displayValue);
    }
}
