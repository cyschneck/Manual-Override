using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PopupEventManager : MonoBehaviour
{
    [Header("Reference")]
    private GameManager gameManager;
    private StatsManager statsManager;

    [Header("Popup Menu and Options")]
    public GameObject popUpMenu;
    public GameObject eventTextBox;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;
    public EventManager eventManager;
    public bool isPopUpActive = false;
    public bool testingToRemovePopupToBeRemoved = false;

    public TextToDisplayEvents[] eventToDisplayInPopupList;
    public TextToDisplayEvents testingEventToDisplayInPopup; // TESTING FUNCTIONALITY TO BE REMOVED

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();

        Debug.Log("TESTING POPUP ON: " + testingEventToDisplayInPopup + " is" + testingEventToDisplayInPopup.popUpMenuOption);
        Debug.Log("TODO TO DO : display tooltip on the contine/yes/no button to info about changes when relevant (when there are any costs)");
    }

    private void Update()
    {
        // for testing purposes triggers an event after 10 seconds (TO BE REMOVED)
        if (gameManager.currentTime >= 10.0f && !isPopUpActive && !testingToRemovePopupToBeRemoved)
        {
            SetUpPopup(testingEventToDisplayInPopup);
            testingToRemovePopupToBeRemoved = true;
        }

        if (isPopUpActive)
        {
            // pause game when popup is active
            gameManager.StopTimePause();
            gameManager.DisableOrEnableAllTermianlButtonsInteractable(false); // make buttons not interactable
        }
    }

    public void SetUpPopup(TextToDisplayEvents displayValue)
    {
        eventTextBox.GetComponent<TextMeshProUGUI>().text = displayValue.eventText;
        if (displayValue.popUpMenuOption == popUpMenuOptions.continueOnly)
        {
            SetUpGenericEvent(displayValue);
        }
        else
        {
            SetUpYesAndNoEvent(displayValue);
        }
        popUpMenu.SetActive(true);
        isPopUpActive = true;
    }

    private void SetUpYesAndNoEvent(TextToDisplayEvents displayValue)
    {
        // set up menu with yes and no options
        continueButton.SetActive(false);
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    private void SetUpGenericEvent(TextToDisplayEvents displayValue)
    {
        // set up menu with just a continue button
        continueButton.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    public void ContinueButton()
    {
        Debug.Log("PRESSED CONTINUE");
        ClosePopUpMenu();
    }

    public void YesButton()
    {
        Debug.Log("PRESSED YES");
        ClosePopUpMenu();
    }

    public void NoButton()
    {
        Debug.Log("PRESSED NO");
        ClosePopUpMenu();
    }

    public void ClosePopUpMenu()
    {
        // close popup menu, restart time, apply event stats, display terminal text
        Debug.Log("TODO: apply event stats, display terminal text");
        gameManager.StartGameFromPause(); // restarts time
        gameManager.DisableOrEnableAllTermianlButtonsInteractable(true); // make buttons not interactable

        popUpMenu.SetActive(false);
        isPopUpActive = false;
        statsManager.UpdateEventValues(testingEventToDisplayInPopup);
    }
}
