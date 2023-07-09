using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorOptionController : MonoBehaviour
{
    public TMP_Text nameText;
    public GameObject baseButton;
    public GameObject luxuryButton;
    public TMP_Text baseButtonText;
    public TMP_Text luxuryButtonText;
    public TMP_Text priceText;

    public enum State
    {
        BASE_SELECTED,
        LUXURY_SELECTED,
    }

    private State currentState;
    private MenuController parentMenuController;
    private int myIndex;

    private int luxuryPrice;

    public void Setup(MenuController parent, int index, string name, State initialState, int luxuryPrice)
    {
        parentMenuController = parent;
        myIndex = index;
        nameText.text = name;
        this.luxuryPrice = luxuryPrice;
        if (initialState == State.BASE_SELECTED)
        {
            SwitchToBase();
        }
        else if (initialState == State.LUXURY_SELECTED)
        {
            SwitchToLuxury();
        }
    }

    private void SwitchToBase()
    {
        priceText.text = "+$0";

        baseButton.transform.localScale = Vector3.one;
        luxuryButton.transform.localScale = Vector3.one * 0.85f;

        baseButtonText.fontStyle = FontStyles.Bold;
        luxuryButtonText.fontStyle = FontStyles.Normal;

        parentMenuController.UpdateFromColorPart(myIndex, false);
        currentState = State.BASE_SELECTED;
    }

    private void SwitchToLuxury()
    {
        priceText.text = string.Format("+${0:#,#}", luxuryPrice);

        baseButton.transform.localScale = Vector3.one * 0.85f;
        luxuryButton.transform.localScale = Vector3.one;

        baseButtonText.fontStyle = FontStyles.Normal;
        luxuryButtonText.fontStyle = FontStyles.Bold;

        parentMenuController.UpdateFromColorPart(myIndex, true);
        currentState = State.LUXURY_SELECTED;
    }

    public void OnBasePressed()
    {
        if (currentState == State.LUXURY_SELECTED)
        {
            SwitchToBase();
        }
    }

    public void OnLuxuryPressed()
    {
        if (currentState == State.BASE_SELECTED)
        {
            SwitchToLuxury();
        }
    }
}
