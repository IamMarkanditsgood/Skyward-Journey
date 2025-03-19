using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static event Action OnFly;
    public static event Action OnShoot;
    public static event Action OnDropBomb;

    public static void Fly() => OnFly?.Invoke();
    public static void Shoot() => OnShoot?.Invoke();
    public static void DropBomb() => OnDropBomb?.Invoke();
}
