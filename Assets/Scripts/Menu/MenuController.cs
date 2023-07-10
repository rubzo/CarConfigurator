using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public CarList allCars;

    public GameObject carMeshParent;
    public TMP_Text carNameText;
    public TMP_Text basePriceText;

    public TMP_Text totalPriceText;

    public GameObject colorPartsContentHolder;
    public GameObject accessoriesContentHolder;

    public GameObject colorPartPrefab;
    public GameObject accessoryPrefab;

    public InvoiceController invoiceController;

    private int currentCarIndex;
    private int carCount;

    private GameObject currentPrefab;
    private CarDesignController currentDesignController;
    private CarDetails currentDetails;

    private bool[] colorPartSelections;
    private bool[] accessorySelections;

    private Carousel carousel;
    private DriveForward driveForward;

    enum State
    {
        WAITING,
        PAID
    }

    private State state;

    void Awake()
    {
        currentCarIndex = 0;
        carousel = carMeshParent.GetComponent<Carousel>();
        carCount = allCars.carList.Count;
        LoadCarFromIndex(currentCarIndex);
    }

    private void RemoveAllChildrenFromContainer(GameObject container)
    {
        foreach (Transform t in container.transform)
        {
            GameObject.Destroy(t.gameObject);
        }
        container.transform.DetachChildren();
    }

    private void UpdateDisplayedTotalPriceBasedOnSelection()
    {
        int total = 0;
        total += currentDetails.basePrice;
        for (int index = 0; index < currentDetails.colorParts.Count; index++)
        {
            if (colorPartSelections[index])
            {
                total += currentDetails.colorParts[index].luxuryPrice;
            }
        }
        for (int index = 0; index < currentDetails.accessories.Count; index++)
        {
            if (accessorySelections[index])
            {
                total += currentDetails.accessories[index].price;
            }
        }

        totalPriceText.text = string.Format("${0:#,#}", total);
    }

    private void LoadCarFromIndex(int index)
    {
        state = State.WAITING;

        carousel.enabled = true;

        RemoveAllChildrenFromContainer(carMeshParent);

        currentPrefab = GameObject.Instantiate(allCars.carList[index], carMeshParent.transform, false);
        currentDesignController = currentPrefab.GetComponent<CarDesignController>();
        currentDetails = currentDesignController.GetDetails();

        driveForward = carMeshParent.transform.GetChild(0).GetComponent<DriveForward>();

        colorPartSelections = new bool[currentDetails.colorParts.Count];
        accessorySelections = new bool[currentDetails.accessories.Count];

        SetAllDetails();
    }

    private void SpawnAllColorParts(List<ColorPart> colorParts)
    {
        for (int colorPartIndex = 0; colorPartIndex < colorParts.Count; colorPartIndex++)
        {
            ColorPart colorPart = colorParts[colorPartIndex];

            GameObject newGameObject = GameObject.Instantiate(colorPartPrefab, colorPartsContentHolder.transform, false);
            ColorPartController controller = newGameObject.GetComponent<ColorPartController>();
            controller.Setup(this, colorPartIndex, colorPart.name, ColorPartController.State.BASE_SELECTED, colorPart.luxuryPrice);
        }
    }

    private void SpawnAllAccessories(List<Accessory> accessories)
    {
        for (int accessoryIndex = 0; accessoryIndex < accessories.Count; accessoryIndex++)
        {
            Accessory accessory = accessories[accessoryIndex];

            GameObject newGameObject = GameObject.Instantiate(accessoryPrefab, accessoriesContentHolder.transform, false);
            AccessoryController controller = newGameObject.GetComponent<AccessoryController>();
            controller.Setup(this, accessoryIndex, accessory.name, AccessoryController.State.UNSELECTED, accessory.price);
        }
    }

    private void SetAllDetails()
    {
        carNameText.text = currentDetails.name;
        basePriceText.text = string.Format("${0:#,#}", currentDetails.basePrice);

        RemoveAllChildrenFromContainer(colorPartsContentHolder);
        SpawnAllColorParts(currentDetails.colorParts);

        RemoveAllChildrenFromContainer(accessoriesContentHolder);
        SpawnAllAccessories(currentDetails.accessories);

        UpdateDisplayedTotalPriceBasedOnSelection();
    }

    private void DispatchInvoice()
    {
        List<string> items = new List<string>();
        List<int> prices = new List<int>();

        items.Add(currentDetails.name);
        prices.Add(currentDetails.basePrice);

        for (int index = 0; index < currentDetails.colorParts.Count; index++)
        {
            if (colorPartSelections[index])
            {
                items.Add("+ Luxury " + currentDetails.colorParts[index].name);
                prices.Add(currentDetails.colorParts[index].luxuryPrice);
            }
        }

        for (int index = 0; index < currentDetails.accessories.Count; index++)
        {
            if (accessorySelections[index])
            {
                items.Add("+ " + currentDetails.accessories[index].name);
                prices.Add(currentDetails.accessories[index].price);
            }
        }

        invoiceController.CreateInvoice(items, prices);
        invoiceController.ShowInvoice();
    }

    //
    // -- Interface --
    //

    public void PrevButtonPressed()
    {
        currentCarIndex--;
        if (currentCarIndex < 0)
        {
            currentCarIndex = carCount - 1;
        }
        LoadCarFromIndex(currentCarIndex);
    }

    public void NextButtonPressed()
    {
        currentCarIndex = (currentCarIndex + 1) % carCount;
        LoadCarFromIndex(currentCarIndex);
    }

    public void UpdateFromColorPart(int index, bool switchToLuxury)
    {
        colorPartSelections[index] = switchToLuxury;
        currentDesignController.AdjustColorPart(index, switchToLuxury);
        UpdateDisplayedTotalPriceBasedOnSelection();
    }

    public void UpdateFromAccessory(int index, bool isNowSelected)
    {
        accessorySelections[index] = isNowSelected;
        UpdateDisplayedTotalPriceBasedOnSelection();
    }

    public void DriveItAwayButtonPressed()
    {
        if (state == State.WAITING)
        {
            carousel.enabled = false;

            DispatchInvoice();

            driveForward.Go();

            state = State.PAID;
        }
    }
}
