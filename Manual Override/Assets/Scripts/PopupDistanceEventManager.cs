using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDistanceEventManager : MonoBehaviour
{
    // Controls Distance-based events for popups

    [Header("Reference")]
    private EventManager eventManager;
    private StatsManager statsManager;
    private PopupEventManager popupEventManager;

    public float distanceTravelled;

    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        popupEventManager = GameObject.Find("PopupEventManager").GetComponent<PopupEventManager>();

        // set all events triggered to false when starting
        foreach (TextToDisplayEvents eventDistanceObject in eventManager.allDistanceEvents)
        {
            eventDistanceObject.eventHasBeenTriggered = false;
        }

        distanceTravelled = 0.0f;
    }

    private void FixedUpdate()
    {
        distanceTravelled = statsManager.titanTotalDistance - statsManager.distanceToTitanAmount;

        foreach (TextToDisplayEvents eventDistanceObject in eventManager.allDistanceEvents)
        {
            if (eventDistanceObject.distanceToTitanInKmTrigger < distanceTravelled)
            {
                // trigger event when the distance to object is reached
                if (!eventDistanceObject.eventHasBeenTriggered) // check that event has not been displayed
                {
                    popupEventManager.SetUpPopup(eventDistanceObject);
                    eventDistanceObject.eventHasBeenTriggered = true;
                }
            }
        }
    }
}
