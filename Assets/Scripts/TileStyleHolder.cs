using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileStyles
{
    public int Number;
    public Color32 TileColor;
    public Color32 TextColor;
}

public class TileStyleHolder : MonoBehaviour
{
    public static TileStyleHolder instance;
    public TileStyles[] tileStyles;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
