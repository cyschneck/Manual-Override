using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terminal Text Object", menuName = "Terminal Text")]
public class TextToDisplay : ScriptableObject
{
    public string description;
    public string text;
    public float cooldownTime;
    public float heatCost;
    public float energyCellCost;
    public float waterCost;
    public float robotCost;
    public float plantCost;
    public float seedsCost;
    public float methaneCost;
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
