using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccessoryController : MonoBehaviour
{
    public TMP_Text nameText;
    public GameObject addButton;
    public TMP_Text addButtonText;
    public TMP_Text priceText;

    public enum State
    {
        UNSELECTED,
        SELECTED,
    }

    private State currentState;
    private MenuController parentMenuController;
    private int myIndex;

    private int selectedPrice;

    public void Setup(MenuController parent, int index, string name, State initialState, int selectedPrice)
    {
        parentMenuController = parent;
        myIndex = index;
        nameText.text = name;
        this.selectedPrice = selectedPrice;
        if (initialState == State.UNSELECTED)
        {
            SwitchToUnselected();
        }
        else if (initialState == State.SELECTED)
        {
            SwitchToSelected();
        }
    }

    private void SwitchToUnselected()
    {
        priceText.text = "+$0";
        addButtonText.text = "Add";

        parentMenuController.UpdateFromAccessory(myIndex, false);
        currentState = State.UNSELECTED;
    }

    private void SwitchToSelected()
    {
        priceText.text = string.Format("+${0:#,#}", selectedPrice);
        addButtonText.text = "Remove";

        parentMenuController.UpdateFromAccessory(myIndex, true);
        currentState = State.SELECTED;
    }

    public void OnAddPressed()
    {
        if (currentState == State.UNSELECTED)
        {
            SwitchToSelected();
        }
        else
        {
            SwitchToUnselected();
        }
    }
}
