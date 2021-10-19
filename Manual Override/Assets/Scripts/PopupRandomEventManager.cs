using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRandomEventManager : MonoBehaviour
{
    [Header("Reference")]
    private EventManager eventManager;
    private PopupEventManager popupEventManager;
    private GameManager gameManager;

    [Header("Timing for Random Events")]
    public float timeSinceRandomEvent;
    private float minTimeBetweenEvents = 15.0f; // seconds
    private float maxTimeBetweenEvents = 20.0f; // seconds
    private float randomFloatOne;
    private float randomFloatTwo;
    public float triggerTime;
    public bool waitingToTriggerEvent = false;

    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        popupEventManager = GameObject.Find("PopupEventManager").GetComponent<PopupEventManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        timeSinceRandomEvent = 0.0f;
    }

    private void Update()
    {
        // calculate time elapsed since last random event displayed
        if (!gameManager.gameIsPaused)
        {
            timeSinceRandomEvent += 1.0f * Time.deltaTime;
            if (!waitingToTriggerEvent && timeSinceRandomEvent > minTimeBetweenEvents)
            {
                // find a random time to wait until after min is passed to display a random event
                randomFloatOne = Random.Range(0.0f, maxTimeBetweenEvents);
                randomFloatTwo = Random.Range(0.0f, maxTimeBetweenEvents);
                triggerTime = timeSinceRandomEvent + (randomFloatOne + randomFloatTwo) / 2; // random event is the average between two points (better distribution)
                waitingToTriggerEvent = true;
            }

            if (waitingToTriggerEvent)
            {
                if (triggerTime < timeSinceRandomEvent) 
                {
                    Debug.Log("triggering random event at : " + triggerTime);
                    TriggerRandomEvent();
                    waitingToTriggerEvent = false;
                }
            }
        }
    }

    private void TriggerRandomEvent()
    {
        // trigger a random event from a list of possible events
        foreach (TextToDisplayEvents eventRandomObject in eventManager.allRandomEvents)
        {
            Debug.Log(eventRandomObject.eventText);
        }

        // reset time elapsed
        timeSinceRandomEvent = 0.0f;
    }
}
