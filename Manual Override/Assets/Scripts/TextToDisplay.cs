﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Terminal Text Object", menuName = "Terminal Text")]
public class TextToDisplay : ScriptableObject
{
    // Scriptable object for terminal buttons

    [Header("Terminal Buttons and Text")]
    public string description;
    public string text;
    public float cooldownTime;
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
}
