using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPaper : MonoBehaviour
{
    public TextMeshPro orderText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
