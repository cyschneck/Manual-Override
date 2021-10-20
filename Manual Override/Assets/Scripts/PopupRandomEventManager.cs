﻿using System.Collections;
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
    private float minTimeBetweenEvents = 10.0f; // seconds (waits this long before attempting to trigger a new event)
    private float maxTimeBetweenEvents = 30.0f; // seconds (sets up the random above min time that will be added: min + random value up to max)
    private float randomFloatOne;
    private float randomFloatTwo;
    public float triggerTime;
    public bool waitingToTriggerEvent = false;
    public int randomWeight;

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
                // if an event has been triggered, wait until the time to trigger is less than the time since the last trigger
                if (triggerTime < timeSinceRandomEvent) 
                {
                    TriggerRandomEvent();
                    waitingToTriggerEvent = false;
                }
            }
        }
    }

    private void TriggerRandomEvent()
    {
        // trigger a random event from a list of possible events

        // collect the sum of all the weights
        int sum_weights = 0;
        foreach (TextToDisplayEvents eventRandomObject in eventManager.allRandomEvents)
        {
            sum_weights += eventRandomObject.weightOfOccuring;
        }

        // choose a random number between 0 and the total sum
        randomWeight = (Random.Range(0, sum_weights) + Random.Range(0, sum_weights)) / 2; // random event is the average between two points (better distribution)
        foreach (TextToDisplayEvents eventRandomObject in eventManager.allRandomEvents)
        {
            if (randomWeight > eventRandomObject.weightOfOccuring)
            {
                // iterate through and reduce the random value by the weight if random number is not less than object's weight
                randomWeight -= eventRandomObject.weightOfOccuring;
            } else
            {
                // trigger event and reset timer
                popupEventManager.SetUpPopup(eventRandomObject);
                // reset time elapsed
                timeSinceRandomEvent = 0.0f;
                return;
            }
        }

    }
}