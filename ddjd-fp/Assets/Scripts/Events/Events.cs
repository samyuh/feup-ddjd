using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events {
    // Player Events
    // Player
    // // Player Events
    public static readonly EventNative OnSpawn = new EventNative(); // TODO
    public static readonly EventNative OnDeath = new EventNative(); // TODO
    // // Update Player Status
    public static readonly EventNative<int, int> OnHealthUpdate = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnManaUpdate = new EventNative<int, int>(); // TODO

    // // Player Actions
    // //
    // // // Catch
    public static readonly EventNative OnCatchManaCrystal = new EventNative();
    public static readonly EventNative OnCatchHealthCrystal = new EventNative();
    // // // Use
    public static readonly EventNative OnUseHealthCrystal = new EventNative();
    public static readonly EventNative OnUseManaCrystal = new EventNative();

    // // // Button
    public static readonly EventNative OnActivatePortal = new EventNative();
    
    // Menu
    //
    // // Toggle States
    public static readonly EventNative OnTogglePauseMenu = new EventNative();
    public static readonly EventNative OnToggleCrystalWheel = new EventNative();
    public static readonly EventNative OnToggleInventory = new EventNative();
    public static readonly EventNative OnToggleAim = new EventNative(); 

    // // // Crystal Wheel
    public static readonly EventNative<CrystalData> OnSetActiveCrystal  = new EventNative<CrystalData>();

    // // // Pause Menu

    // // // Inventory
    public static readonly EventNative OnChangeCrystalSlots = new EventNative(); 

    // // // Pressure Plate
     public static readonly EventNative OnPressurePlate  = new EventNative();
}
