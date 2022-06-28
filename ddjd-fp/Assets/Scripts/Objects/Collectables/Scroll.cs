using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scroll")]
public class ScrollData: ScriptableObject {
    public int id;
    public string name;
    public Unity.VectorGraphics.SVGImage icon;
}