﻿using Mirror;
using UnityEngine;

public class GamePlayer : NetworkBehaviour
{
    [SyncVar]
    public int index;

    [SyncVar]
    public uint score;

    void OnGUI()
    {
        GUI.Box(new Rect(10f + (index * 110), 10f, 100f, 25f), $"P{index}: {score:0000000}");
    }
}
