using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Events Text Options")]
    public TextToDisplayEvents cmeBreaksElectronics;
    public TextToDisplayEvents enteredGravityWell;
    public TextToDisplayEvents escapeGravityWell;
    public TextToDisplayEvents micrometeoriteBreaksEngineering;
    public TextToDisplayEvents plantsGetSick;
    public TextToDisplayEvents fireInEngineeringFromOxygen;
    public TextToDisplayEvents poorNitrogenSoilKillsPlants;
    public TextToDisplayEvents lowWaterKillsPlants;
    public TextToDisplayEvents approachingMars;
    public TextToDisplayEvents apporoachingAsteroidBelt;
    public TextToDisplayEvents approachingJupiter;
    public TextToDisplayEvents approachingJupitersMoons;
    public TextToDisplayEvents approachingSaturn;
    public TextToDisplayEvents approachingTitan;
    public TextToDisplayEvents randomFireInEngineering;
    public List<TextToDisplayEvents> allEvents = new List<TextToDisplayEvents>();
    public List<TextToDisplayEvents> allRandomEvents = new List<TextToDisplayEvents>();

    private void Awake()
    {
        // keep track of all events
        allEvents.Add(cmeBreaksElectronics);
        allEvents.Add(enteredGravityWell);
        allEvents.Add(escapeGravityWell);
        allEvents.Add(micrometeoriteBreaksEngineering);
        allEvents.Add(plantsGetSick);
        allEvents.Add(fireInEngineeringFromOxygen);
        allEvents.Add(poorNitrogenSoilKillsPlants);
        allEvents.Add(lowWaterKillsPlants);
        allEvents.Add(apporoachingAsteroidBelt);
        allEvents.Add(approachingJupiter);
        allEvents.Add(approachingJupitersMoons);
        allEvents.Add(approachingSaturn);
        allEvents.Add(approachingTitan);

        // iterate through all events to track all random events
        foreach(TextToDisplayEvents eventObject in allEvents)
        {
            if (eventObject.eventType == eventType.random)
            {
                allRandomEvents.Add(eventObject);
            }
        }

    }

    public void TriggerRandomEvent()
    {
        // trigger a random event

    }
}
