using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DayAndNightPreset", menuName = "Scriptable / DANPreset", order = 1)]


public class DANPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient fogColor;
    //public Gradient SunIntensity;
}

