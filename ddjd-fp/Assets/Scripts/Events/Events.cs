using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events {
    // Player Events
    public static readonly EventNative OnSpawn = new EventNative(); // TODO
    public static readonly EventNative OnDeath = new EventNative(); // TODO
    public static readonly EventNative<int, int> OnHealthUpdate = new EventNative<int, int>();

    public static readonly EventNative OnCatchCrystal = new EventNative();
    public static readonly EventNative OnCatchHealthPlant = new EventNative();
    
    // Menu
    //
    // // Crystal Wheel
    public static readonly EventNative OnToggleCrystalWheel = new EventNative();
    public static readonly EventNative<int> OnChangeSelectedCrystal  = new EventNative<int>();
}
