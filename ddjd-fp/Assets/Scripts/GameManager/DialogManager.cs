using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Dialog Manager")]
public class DialogManager: ScriptableObject {
    public int id;
    public List<string> dialog;
}