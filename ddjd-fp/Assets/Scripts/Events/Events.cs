using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events {
    // Player Events
    public static readonly EventNative<int, int> OnSpawn = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnDeath = new EventNative<int, int>();
    
    public static readonly EventNative<int, int> OnTakeDamage = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnCatchCrystal = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnCatchHealthPlant = new EventNative<int, int>();
    public static readonly EventNative<int, int> OnHealthUpdate = new EventNative<int, int>();

    // Input Events
    public static readonly EventNative OnInteract = new EventNative();
    public static readonly EventNative OnJump = new EventNative();
    public static readonly EventNative OnAttack = new EventNative();
    public static readonly EventNative<Vector2> OnLook = new EventNative<Vector2>();
    public static readonly EventNative<Vector2> OnMove = new EventNative<Vector2>();
}