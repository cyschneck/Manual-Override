using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAccidentManager : MonoBehaviour
{
    [Header("Reference")]
    private GameManager gameManager;
    private EventManager eventManager;
    private StatsManager statsManager;
    private PopupEventManager popupEventManager;

    [Header("Track Accidents And Events")]
    public float timeSinceLastAccident;
    public bool hasHadRecentAccident;
    private float waitBetweenChecks = 15.0f; // time to wait befor echecking again

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        popupEventManager = GameObject.Find("PopupEventManager").GetComponent<PopupEventManager>();
        hasHadRecentAccident = false;
    }

    private void Update()
    {
        // calculate time elapsed since last random event displayed
        if (!gameManager.gameIsPaused)
        {
            timeSinceLastAccident += 1.0f * Time.deltaTime;

            if (timeSinceLastAccident > waitBetweenChecks)
            {
                foreach (TextToDisplayEvents eventAccidentConditionsObject in eventManager.allAccientConditionsEvents)
                {
                    //Debug.Log(eventAccidentConditionsObject);
                    TriggerEventBasedOnStats(eventAccidentConditionsObject);
                }
            }

            if (hasHadRecentAccident)
            {
                timeSinceLastAccident = 0.0f;
                hasHadRecentAccident = false;
            }
        }
    }

    public void TriggerEventBasedOnStats(TextToDisplayEvents eventObject)
    {
        // check stats to determine if an event should be triggered
        if (statsManager.nitrogenValue <= eventObject.nitrogenMinTrigger || statsManager.nitrogenValue >= eventObject.nitrogenMaxTrigger)
        {
            popupEventManager.SetUpPopup(eventObject);
            hasHadRecentAccident = true;
        }
        if (statsManager.oxygenValue <= eventObject.oxygenMinTrigger || statsManager.oxygenValue >= eventObject.oxygenMaxTrigger)
        {
            popupEventManager.SetUpPopup(eventObject);
            hasHadRecentAccident = true;

        }
        if (statsManager.carbonDioxdeValue <= eventObject.carbonDioxdeMinTrigger || statsManager.carbonDioxdeValue >= eventObject.carbonDioxdeMaxTrigger)
        {
            popupEventManager.SetUpPopup(eventObject);
            hasHadRecentAccident = true;

        }
        if (statsManager.hydrogenValue <= eventObject.hydrogenMinTrigger || statsManager.hydrogenValue >= eventObject.hydrogenMaxTrigger)
        {
            popupEventManager.SetUpPopup(eventObject);
            hasHadRecentAccident = true;
        }
    }
}
