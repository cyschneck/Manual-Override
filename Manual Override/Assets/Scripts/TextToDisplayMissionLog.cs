using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission Log Object", menuName = "Mission Log")]
public class TextToDisplayMissionLog : ScriptableObject
{
    // Scriptable object for Mission Logs

    [Header("Mission Logs")]
    public string missionDescription;
    [TextArea]
    public string missionText;
}
