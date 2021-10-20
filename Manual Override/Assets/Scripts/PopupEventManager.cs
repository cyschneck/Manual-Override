using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PopupEventManager : MonoBehaviour
{
    [Header("Reference")]
    private GameManager gameManager;
    private StatsManager statsManager;
    private TerminalTextManager terminalTextManager;


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

        Debug.Log("TODO TO DO : display tooltip on the contine/yes/no button to info about changes when relevant (when there are any costs)");
    }

    private void Update()
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
        eventTextBox.GetComponent<TextMeshProUGUI>().text = eventObject.eventText;
        if (eventObject.popUpMenuOption == popUpMenuOptions.continueOnly)
        {
            SetUpGenericEvent();
        }
        else
        {
            SetUpYesAndNoEvent();
        }
        popUpMenu.SetActive(true);
        isPopUpActive = true;
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
        StartCoroutine(ClosePopUpMenu());
    }

    public void YesButton()
    {
        Debug.Log("PRESSED YES");
        StartCoroutine(ClosePopUpMenu());
    }

    public void NoButton()
    {
        Debug.Log("PRESSED NO");
        StartCoroutine(ClosePopUpMenu());
    }

    public IEnumerator ClosePopUpMenu()
    {
        // close popup menu, restart time, apply event stats, display terminal text
        Debug.Log("TODO: apply event stats, display terminal text");
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
