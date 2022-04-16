using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Camera {
    protected GameObject _mainCamera;

    public abstract void LateUpdateCamera(float magnitude, float lookAxisX, float lookAxisY);
}
