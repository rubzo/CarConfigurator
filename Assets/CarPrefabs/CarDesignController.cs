using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDesignController : MonoBehaviour
{
    public CarDetails carDetails;

    void Awake()
    {
        ResetAllColors();
    }

    public CarDetails GetDetails()
    {
        return carDetails;
    }

    public void ResetAllColors()
    {
        foreach (ColorPart part in carDetails.colorParts)
        {
            part.material.color = part.colorBase;
        }
    }

    public void AdjustColorPart(int index, bool switchToLuxury)
    {
        if (index < 0 || index >= carDetails.colorParts.Count)
        {
            Debug.LogWarningFormat("Invalid index given to AdjustColorPart: {} (valid=0-{})", index, carDetails.colorParts.Count - 1);
            return;
        }

        if (!switchToLuxury)
        {
            carDetails.colorParts[index].material.color = carDetails.colorParts[index].colorBase;
        }
        else
        {
            carDetails.colorParts[index].material.color = carDetails.colorParts[index].colorLuxury;
        }
    }
}
