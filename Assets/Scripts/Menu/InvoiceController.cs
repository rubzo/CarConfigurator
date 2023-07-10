using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvoiceController : MonoBehaviour
{
    public TMP_Text itemListText;
    public TMP_Text priceListText;
    public TMP_Text totalPriceText;

    public GameObject noRefundsText;

    public void CreateInvoice(List<string> itemNames, List<int> itemPrices)
    {
        itemListText.text = string.Join("\n", itemNames);

        List<string> priceTexts = new List<string>();

        int totalPrice = 0;
        foreach (int price in itemPrices)
        {
            totalPrice += price;
            priceTexts.Add(string.Format("${0:#,#}", price));
        }

        priceListText.text = string.Join("\n", priceTexts);

        totalPriceText.text = string.Format("${0:#,#}", totalPrice);

        noRefundsText.SetActive(false);
    }

    public void ShowInvoice()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowNoRefundsAfterDelay());
    }

    private IEnumerator ShowNoRefundsAfterDelay()
    {
        yield return new WaitForSeconds(1.4f);
        noRefundsText.SetActive(true);
    }

    public void HideInvoice()
    {
        gameObject.SetActive(false);
    }
}
