using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events {
    // Player Events
    // Player
    // // Dialog CutScene
    public static readonly EventNative OnNextDialog = new EventNative();
    public static readonly EventNative<DialogManager> OnDialog = new EventNative<DialogManager>();
    public static readonly EventNative FinishDialog = new EventNative();

    // // Player Events 
    public static readonly EventNative OnDeath = new EventNative(); 
    public static readonly EventNative<int> OnBossBattle = new EventNative<int>();
    public static readonly EventNative<int> OnCleanZone = new EventNative<int>();

    // // Update Player Status
    public static readonly EventNative<int, int> OnHealthUpdate = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnCrystalManaUpdate = new EventNative<int, int>(); // TODO

    // // Player Actions
    // // 
    // // // Catch
    public static readonly EventNative OnCatchCrystal = new EventNative();
    public static readonly EventNative OnCatchScroll = new EventNative();
    public static readonly EventNative OnCatchHealthCrystal = new EventNative();
    // // // Use
    public static readonly EventNative OnUseHealthCrystal = new EventNative();
    // // // Button
    public static readonly EventNative<int> OnActivatePortal = new EventNative<int>();
    public static readonly EventNative<int> OnDeactivatePortal = new EventNative<int>();

    // Menu
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
