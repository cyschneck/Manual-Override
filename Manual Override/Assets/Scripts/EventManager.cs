using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Events Text Options")]
    public TextToDisplayEvents approachingMoon;
    public TextToDisplayEvents approachingMars;
    public TextToDisplayEvents apporoachingAsteroidBelt;
    public TextToDisplayEvents approachingJupiter;
    public TextToDisplayEvents approachingJupitersMoons;
    public TextToDisplayEvents approachingSaturn;
    public TextToDisplayEvents approachingTitan;
    public TextToDisplayEvents brokenRobotPiece;
    public TextToDisplayEvents computerError;
    public TextToDisplayEvents computerVirus;
    public TextToDisplayEvents cmeBreaksElectronics;
    public TextToDisplayEvents dirtInFilters;
    public TextToDisplayEvents engineOverheating;
    public TextToDisplayEvents enteredGravityWell;
    public TextToDisplayEvents escapeGravityWell;
    public TextToDisplayEvents fireInEngineeringFromOxygen;
    public TextToDisplayEvents heatFailsHot;
    public TextToDisplayEvents heatFailsCold;
    public TextToDisplayEvents lowWaterKillsPlants;
    public TextToDisplayEvents micrometeoriteBreaksEngineering;
    public TextToDisplayEvents oxygenLeak;
    public TextToDisplayEvents plantsGetSick;
    public TextToDisplayEvents poorNitrogenSoilKillsPlants;
    public TextToDisplayEvents radiationShield;
    public TextToDisplayEvents randomFireInEngineering;

    public List<TextToDisplayEvents> allEvents = new List<TextToDisplayEvents>();
    public List<TextToDisplayEvents> allDistanceEvents = new List<TextToDisplayEvents>();
    public List<TextToDisplayEvents> allRandomEvents = new List<TextToDisplayEvents>();

    private void Awake()
    {
        // keep track of all events
        allEvents.Add(approachingMoon);
        allEvents.Add(approachingMars);
        allEvents.Add(apporoachingAsteroidBelt);
        allEvents.Add(approachingJupiter);
        allEvents.Add(approachingJupitersMoons);
        allEvents.Add(approachingSaturn);
        allEvents.Add(approachingTitan);

        allEvents.Add(brokenRobotPiece);
        allEvents.Add(computerError);
        allEvents.Add(computerVirus);
        allEvents.Add(cmeBreaksElectronics);
        allEvents.Add(dirtInFilters);
        allEvents.Add(engineOverheating);
        allEvents.Add(enteredGravityWell);
        allEvents.Add(escapeGravityWell);
        allEvents.Add(fireInEngineeringFromOxygen);
        allEvents.Add(heatFailsHot);
        allEvents.Add(heatFailsCold);
        allEvents.Add(lowWaterKillsPlants);
        allEvents.Add(micrometeoriteBreaksEngineering);
        allEvents.Add(oxygenLeak);
        allEvents.Add(plantsGetSick);
        allEvents.Add(poorNitrogenSoilKillsPlants);
        allEvents.Add(radiationShield);
        allEvents.Add(randomFireInEngineering);

        // iterate through all events to track all random events
        foreach(TextToDisplayEvents eventObject in allEvents)
        {
            if (eventObject.eventType == eventType.random)
            {
                allRandomEvents.Add(eventObject);
            }
            if (eventObject.eventType == eventType.distanceFromTitan)
            {
                allDistanceEvents.Add(eventObject);
            }
        }

    }
}
