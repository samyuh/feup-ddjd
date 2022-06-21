using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog {
    public string character;
    public string text;
}
 
[CreateAssetMenu(menuName="Dialog Manager")]
public class DialogManager: ScriptableObject {
    public int id;
    public List<Dialog> dialog;
}