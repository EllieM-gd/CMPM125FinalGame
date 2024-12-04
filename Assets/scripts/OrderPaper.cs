using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaper : MonoBehaviour
{
    public TextMeshPro orderText;

    public void setupOrder(int tableNumber, List<string> sauces)
    {
        string orderString = "Table# " + tableNumber + "\n\n";
        foreach (string sauce in sauces)
        {
            orderString += sauce + "\n";
        }
        orderText.text = orderString;
    }
    public void destroyOrder()
    {
        Destroy(gameObject);
    }
}
