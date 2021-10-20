using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission Log Object", menuName = "Mission Log")]
public class TextToDisplayMissionLog : ScriptableObject
{
    public string missionDescription;
    [TextArea]
    public string missionText;
}
