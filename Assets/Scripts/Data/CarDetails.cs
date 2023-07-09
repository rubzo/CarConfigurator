using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct CarDetails
{
    public string name;
    public int basePrice;
    public List<ColorPart> colorParts;
    public List<Accessory> accessories;
}