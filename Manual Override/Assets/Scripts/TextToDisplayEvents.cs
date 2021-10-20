using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eventType { random, distanceFromTitan, conditionsAndAccidents}
public enum popUpMenuOptions { continueOnly, yesOrNo}

[CreateAssetMenu(fileName = "Event Text Object", menuName = "Event Text")]
public class TextToDisplayEvents : ScriptableObject
{
    public string eventDescription;
    [TextArea]
    public string eventText;
    [TextArea]
    public string eventTerminalText;
    public bool eventHasBeenTriggered;
    public eventType eventType;
    public popUpMenuOptions popUpMenuOption;
    [Range(1, 100)]
    public int weightOfOccuring;
    [Range(0.0f, 1277000000.0f)] // 1277000000.0f km (1 billion 277 million from Earth)
    public float distanceToTitanInKmTrigger;
    public float heatCost;
    public float energyCellCost;
    public float waterCost;
    public float robotCost;
    public float plantCost;
    public float seedsCost;
    public float nitrogenCost;
    public float oxygenCost;
    public float carbonDioxdeCost;
    public float hydrogenCost;
    public float chemicalsCost;
    public float copperWireCost;
    public float metalCost;
    public float deadBatteryCost;
    public bool turnEngineOff;
}
