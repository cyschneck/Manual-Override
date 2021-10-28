using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAccidentManager : MonoBehaviour
{
    // Controls Accident-based events for popups

    [Header("Reference")]
    private GameManager gameManager;
    private EventManager eventManager;
    private StatsManager statsManager;
    private PopupEventManager popupEventManager;

    [Header("Track Accidents And Events")]
    public float timeSinceLastAccident;
    public bool resetTimeSinceAccident;
    public float waitBetweenChecks; // time to wait before checking again

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        popupEventManager = GameObject.Find("PopupEventManager").GetComponent<PopupEventManager>();
        resetTimeSinceAccident = false;
        waitBetweenChecks = 25.0f; // first time waits x time
    }

    private void FixedUpdate()
    {
        // calculate time elapsed since last random event displayed
        if (!gameManager.gameIsPaused)
        {
            timeSinceLastAccident += 1.0f * Time.deltaTime;

            if (timeSinceLastAccident > waitBetweenChecks)
            {
                foreach (TextToDisplayEvents eventAccidentConditionsObject in eventManager.allAccidentEvents)
                {
                    //Debug.Log(eventAccidentConditionsObject);
                    TriggerEventBasedOnStats(eventAccidentConditionsObject);
                    if (resetTimeSinceAccident)
                    {
                        // trigger popup if event triggered by stats
                        popupEventManager.SetUpPopup(eventAccidentConditionsObject);
                        timeSinceLastAccident = 0.0f;
                        resetTimeSinceAccident = false;
                    }
                }
            }
        }
    }

    public void TriggerEventBasedOnStats(TextToDisplayEvents eventObject)
    {
        // check stats to determine if an event should be triggered
        if (eventObject.nitrogenMinTrigger <= statsManager.nitrogenValue && eventObject.nitrogenMaxTrigger >= statsManager.nitrogenValue)
        {
            resetTimeSinceAccident = true;
        }
        if (eventObject.oxygenMinTrigger <= statsManager.oxygenValue && eventObject.oxygenMaxTrigger >= statsManager.oxygenValue)
        { 
            resetTimeSinceAccident = true;

        }
        if (eventObject.carbonDioxdeMinTrigger <= statsManager.carbonDioxdeValue && eventObject.carbonDioxdeMaxTrigger >= statsManager.carbonDioxdeValue)
        {
            resetTimeSinceAccident = true;

        }
        if (eventObject.hydrogenMinTrigger <= statsManager.hydrogenValue && eventObject.hydrogenMaxTrigger >= statsManager.hydrogenValue)
        {
            resetTimeSinceAccident = true;
        }
    }
}
