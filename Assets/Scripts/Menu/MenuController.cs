using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public CarList allCars;

    public GameObject carMeshParent;
    public TMP_Text carNameText;

    private int currentCarIndex = 0;
    private int maxIndex;

    private GameObject currentPrefab;
    private CarDesignController currentDesignController;
    private CarDetails currentDetails;

    private void LoadCarFromIndex(int index)
    {
        carMeshParent.transform.DetachChildren();

        currentPrefab = GameObject.Instantiate(allCars.carList[index], carMeshParent.transform, false);
        currentDesignController = currentPrefab.GetComponent<CarDesignController>();
        currentDetails = currentDesignController.GetDetails();

        SetAllDetails();
    }

    private void SetAllDetails()
    {
        carNameText.text = currentDetails.name;
    }

    void Awake()
    {
        maxIndex = allCars.carList.Count - 1;
        LoadCarFromIndex(currentCarIndex);
    }

    public void PrevButtonPressed()
    {

    }

    public void NextButtonPressed()
    {

    }
}
