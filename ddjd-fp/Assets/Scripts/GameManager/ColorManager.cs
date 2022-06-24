using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSwatch {
    public string Name;
    [ColorUsage(true, true)]
    public Color HDR;
}
 
[CreateAssetMenu(menuName="Color Manager")]
public class ColorManager: ScriptableObject {
    public List<ColorSwatch> ColorList;

    public Color getColor(string name) {
        return ColorList.Find(x => x.Name == name).HDR;
    } 
}