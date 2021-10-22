using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Mission Logs and Events Populated By Hand")]
    public TextToDisplayEvents approachingMoon;
    public TextToDisplayEvents approachingMars;
    public TextToDisplayEvents apporoachingAsteroidBelt;
    public TextToDisplayEvents approachingJupiter;
    public TextToDisplayEvents approachingJupitersMoons;
    public TextToDisplayEvents approachingSaturn;
    public TextToDisplayEvents approachingTitan;
    public List<TextToDisplayEvents> allRandomEvents = new List<TextToDisplayEvents>();
    public List<TextToDisplayEvents> allAccidentEvents = new List<TextToDisplayEvents>();

    [Header("Populated by Script")]
    public List<TextToDisplayEvents> allEvents = new List<TextToDisplayEvents>();
    public List<TextToDisplayEvents> allDistanceEvents = new List<TextToDisplayEvents>();

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

        foreach (TextToDisplayEvents eventObject in allRandomEvents)
        {
            allEvents.Add(eventObject);
        }

        foreach (TextToDisplayEvents eventObject in allAccidentEvents)
        {
            allEvents.Add(eventObject);
        }

        // iterate through all events to track all random events
        foreach (TextToDisplayEvents eventObject in allEvents)
        {
            if (eventObject.eventType == eventType.distanceFromTitan)
            {
                allDistanceEvents.Add(eventObject);
            }
        }

    }
}
