using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eventType { random, distanceFromTitan, accidents}
public enum popUpMenuOptions { continueOnly, yesOrNo}

[CreateAssetMenu(fileName = "Event Text Object", menuName = "Event Text")]
public class TextToDisplayEvents : ScriptableObject
{
    // Scriptable object for Event based objects: random, distance to Titan, Accidents

    [Header("Generic")]
    public string eventDescription;
    [TextArea]
    public string eventText;
    [TextArea]
    public string eventTerminalText;
    public bool eventHasBeenTriggered;
    public eventType eventType;
    public popUpMenuOptions popUpMenuOption;
    [Header("Stats")]
    public float hydrogenCellCost;
    public float waterCost;
    public float nitrogenCost;
    public float oxygenCost;
    public float carbonDioxdeCost;
    public float hydrogenCost;
    public float seedsCost;
    public float plantsCost;
    public float methaneCost;
    public float chemicalsCost;
    public float robotCost;
    public float copperWireCost;
    public float metalCost;
    public bool turnEngineOff;
    [Header("Random Events")]
    [Range(1, 100)]
    public int weightOfOccuring;
    [Header("Distance Based Events")]
    [Range(0.0f, 1277000000.0f)] // 1277000000.0f km (1 billion 277 million from Earth)
    public float distanceToTitanInKmTrigger;
    [Header("Accidents and Conditions")]
    [Range(0, 100)]
    public float nitrogenMinTrigger; // low nitrogen trigger
    [Range(0, 100)]
    public float nitrogenMaxTrigger; // high nitrogen trigger
    [Range(0, 100)]
    public float oxygenMinTrigger; // low oxygen trigger
    [Range(0, 100)]
    public float oxygenMaxTrigger; // high oxygen trigger
    [Range(0, 100)]
    public float carbonDioxdeMinTrigger; // low co2 trigger
    [Range(0, 100)]
    public float carbonDioxdeMaxTrigger; // high co2 trigger
    [Range(0, 100)]
    public float hydrogenMinTrigger; // low hydrogen trigger
    [Range(0, 100)]
    public float hydrogenMaxTrigger; // high hydrogen trigger
}
