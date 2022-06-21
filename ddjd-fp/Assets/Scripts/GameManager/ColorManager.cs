using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSwatch {
    public string name;
    public Color rgb;
}
 
[CreateAssetMenu(menuName="Color Manager")]
public class ColorManager: ScriptableObject {
    public List<ColorSwatch> ColorList;
}