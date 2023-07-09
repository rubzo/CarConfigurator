using System;
using UnityEngine;

[Serializable]
public struct ColorPart
{
    public string name;
    public Color colorBase;
    public Color colorLuxury;
    public Material material;
    public int luxuryPrice;
}