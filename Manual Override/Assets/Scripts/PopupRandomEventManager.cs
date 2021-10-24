using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRandomEventManager : MonoBehaviour
{
    // Controls Random-based events for popups

    [Header("Reference")]
    private EventManager eventManager;
    private PopupEventManager popupEventManager;
    private GameManager gameManager;
    private StatsManager statsManager;

    [Header("Timing for Random Events")]
    public float timeSinceRandomEvent;
    public float triggerTime;
    private float minTimeBetweenEvents = 10.0f; // seconds (waits this long before attempting to trigger a new event)
    private float maxTimeBetweenEvents = 10.0f; // seconds (sets up the random above min time that will be added: min + random value up to max)
    private float randomFloatOne;
    private float randomFloatTwo;
    private bool waitingToTriggerEvent = false;
    private int randomWeight;
    public List<TextToDisplayEvents> randomEventsToSelectFrom = new List<TextToDisplayEvents>(); // keep a list of events that will be selected from (sublist of all random events)
    public List<TextToDisplayEvents> mostRecentRandomEvents = new List<TextToDisplayEvents>();

    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        popupEventManager = GameObject.Find("PopupEventManager").GetComponent<PopupEventManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        statsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();

        timeSinceRandomEvent = 0.0f;
    }

    private void FixedUpdate()
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

    private bool DoesEventHaveRelevantStats(TextToDisplayEvents eventRandomObject)
    {
        // only include random events to occur when there are relevant stats (verify stats have a value)
        // all random events have a negative value associated
        // currently will throw the random event if any of the values exist, does not need all

        bool hasEnergyCell = true;
        bool hasWater = true;
        bool hasRobot = true;
        bool hasPlant = true;
        bool hasSeeds = true;
        bool hasMethane = true;
        bool hasNitrogen = true;
        bool hasOxygen = true;
        bool hasCarbonDioxde = true;
        bool hasHydrogen = true;
        bool hasChemicals = true;
        bool hasCooperWire = true;
        bool hasMetal = true;
        bool hasDeadBattery = true;

        if (eventRandomObject.energyCellCost != 0) { if (statsManager.energyCellAmount == 0) { hasEnergyCell = false; }}
        if (eventRandomObject.waterCost != 0) { if (statsManager.waterAmount == 0) { hasWater = false; }}
        if (eventRandomObject.robotCost != 0) { if (statsManager.robotsAmount == 0) { hasRobot = false; }}
        if (eventRandomObject.plantCost != 0) { if (statsManager.plantsAmount == 0) { hasPlant = false; }}
        if (eventRandomObject.seedsCost != 0) { if (statsManager.seedsAmount == 0) { hasSeeds = false; }}
        if (eventRandomObject.methaneCost != 0) { if (statsManager.methaneAmount == 0) { hasMethane = false; }}
        if (eventRandomObject.nitrogenCost != 0) { if (statsManager.nitrogenValue == 0) { hasNitrogen = false; }}
        if (eventRandomObject.oxygenCost != 0) { if (statsManager.oxygenValue == 0) { hasOxygen = false; }}
        if (eventRandomObject.carbonDioxdeCost != 0) { if (statsManager.carbonDioxdeValue == 0) { hasCarbonDioxde = false; }}
        if (eventRandomObject.hydrogenCost != 0) { if (statsManager.hydrogenValue == 0) { hasHydrogen = false; }}
        if (eventRandomObject.chemicalsCost != 0) { if (statsManager.chemicalsAmount == 0) { hasChemicals = false; }}
        if (eventRandomObject.copperWireCost != 0) { if (statsManager.copperWireAmount == 0) { hasCooperWire = false; }}
        if (eventRandomObject.metalCost != 0) { if (statsManager.metalAmount == 0) { hasMetal = false; }}
        if (eventRandomObject.deadBatteryCost != 0) { if (statsManager.deadBatteryAmount == 0) { hasDeadBattery = false; }}

        return (hasEnergyCell && hasWater && hasRobot && hasPlant && hasSeeds && hasMethane && hasNitrogen && hasOxygen && hasCarbonDioxde && hasHydrogen && hasChemicals && hasCooperWire && hasMetal && hasDeadBattery);
    }

    private void TriggerRandomEvent()
    {
        // trigger a random event from a list of possible events
        randomEventsToSelectFrom = new List<TextToDisplayEvents>(); // reset list

        // collect the sum of all the weights
        int sum_weights = 0;
        foreach (TextToDisplayEvents eventRandomObject in eventManager.allRandomEvents)
        {
            if (!mostRecentRandomEvents.Contains(eventRandomObject)) // prevent repeating events (tracks the last x)
            {
                // only include random events when those stats exist (example: remove plants when plants > x)
                if (DoesEventHaveRelevantStats(eventRandomObject))
                {
                    randomEventsToSelectFrom.Add(eventRandomObject);
                    sum_weights += eventRandomObject.weightOfOccuring;
                }
            }
        }

        // choose a random number between 0 and the total sum
        randomWeight = (Random.Range(0, sum_weights) + Random.Range(0, sum_weights)) / 2; // random event is the average between two points (better distribution)
        foreach (TextToDisplayEvents eventRandomObject in randomEventsToSelectFrom)
        {
            if (randomWeight > eventRandomObject.weightOfOccuring)
            {
                // iterate through and reduce the random value by the weight if random number is not less than object's weight
                randomWeight -= eventRandomObject.weightOfOccuring;
            } else
            {
                // trigger event and reset timer and set stats based on the object
                //Debug.Log("Random event triggered = " + eventRandomObject.eventDescription);
                popupEventManager.SetUpPopup(eventRandomObject);
                KeepTrackOfRandomEvents(eventRandomObject);

                // reset time elapsed
                timeSinceRandomEvent = 0.0f;
                return;
            }
        }

    }

   private void KeepTrackOfRandomEvents(TextToDisplayEvents recentRandomObject)
    {
        // keep track of x most recent events to prevent the same event from being triggered too often
        mostRecentRandomEvents.Add(recentRandomObject);
        if (mostRecentRandomEvents.Count > 3)
        {
            // only keep the 3 most recent events, remove oldest event
            mostRecentRandomEvents.RemoveAt(0);
        }
    }
}
