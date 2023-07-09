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
    private int carCount;

    private GameObject currentPrefab;
    private CarDesignController currentDesignController;
    private CarDetails currentDetails;

    private void LoadCarFromIndex(int index)
    {
        foreach (Transform t in carMeshParent.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
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
        carCount = allCars.carList.Count;
        LoadCarFromIndex(currentCarIndex);
    }

    public void PrevButtonPressed()
    {
        currentCarIndex = (currentCarIndex - 1) % carCount;
        LoadCarFromIndex(currentCarIndex);
    }

    public void NextButtonPressed()
    {
        currentCarIndex = (currentCarIndex + 1) % carCount;
        LoadCarFromIndex(currentCarIndex);
    }
}
