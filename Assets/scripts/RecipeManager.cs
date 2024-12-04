using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using JetBrains.Annotations;
using System.Collections;

public class RecipeManager : MonoBehaviour
{
    public List<string> availableSauces = new List<string> { "Tomato", "Alfredo", "Pesto" }; // Sauce options
    public static RecipeManager Instance;
    public GameObject bulletinBoard;
    public GameObject orderPaperPrefab;
    public float timeToGenerateNewRecipe = 15f;

    public class Order
    {
        public List<string> sauces;
        public int tableNumber;
        public int boardSlot;

        public Order(List<string> sauces, int tableNumber)
        {
            this.sauces = sauces;
            this.tableNumber = tableNumber;
        }

        public bool IsEqualOrder(List<string> other)
        {
            // Check if the sauces are the same, regardless of the order
            if (other == null) return false;
            if (sauces.Count != other.Count) return false;
            HashSet<string> sauceSet = new HashSet<string>(sauces);
            foreach (string sauce in other)
            {
                if (!sauceSet.Contains(sauce)) return false;
            }
            return true;
        }
        public void debugOrder()
        {
            Debug.Log("Sauces in order: " + string.Join(", ", sauces));
        }
    }

    public class Board {
        public List<Order> orders = new List<Order>();
        public bool[] boardSlots = new bool[8];

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                boardSlots[i] = false;
            }
        }

        public int getAvailableSlot()
        {
            var availableSlots = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                if (!boardSlots[i])
                {
                    availableSlots.Add(i);
                }
            }
            if (availableSlots.Count == 0) return -1;
            return availableSlots[UnityEngine.Random.Range(0, availableSlots.Count)];
        }
        public void removeOrder(Order order)
        {
            orders.Remove(order);
            boardSlots[order.boardSlot] = false;
        }
        public void AddOrder(Order order, Transform Parent, int tempBoardSlot)
        {
            order.boardSlot = tempBoardSlot;
            orders.Add(order);
            GameObject orderPaper = Instantiate(Instance.orderPaperPrefab, Parent);
            orderPaper.GetComponent<OrderPaper>().setupOrder(order.tableNumber, order.sauces);
            boardSlots[order.boardSlot] = true;
        }
        public bool DoesOrderExist(List<string> order)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].IsEqualOrder(order)) return true;
            }
            return false;
        }
        public Order returnOrder(List<string> order)
        {
            foreach (Order o in orders)
            {
                if (o.IsEqualOrder(order)) return o;
            }
            return null;
        }
    }
    public Board board = new Board();


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GenerateNewRecipe();
        StartCoroutine(WaitAndGenerateNewRecipe());
    }

    IEnumerator WaitAndGenerateNewRecipe()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToGenerateNewRecipe);
            GenerateNewRecipe();
        }
    }

    // Generate a new recipe with random sauces
    public void GenerateNewRecipe()
    {
        List<string> currentRecipe = new List<string>();
        List<string> availableSaucesCopy = new List<string>(availableSauces); // Create a copy to ensure no duplicates
        int numberOfSauces = Random.Range(2, 4); // Recipe includes 2-3 sauces

        for (int i = 0; i < numberOfSauces; i++)
        {
            if (availableSaucesCopy.Count == 0) break;
            int randomIndex = Random.Range(0, availableSaucesCopy.Count);

            string randomSauce = availableSaucesCopy[randomIndex];
            currentRecipe.Add(randomSauce);

            availableSaucesCopy.RemoveAt(randomIndex);
        }
        int slot = board.getAvailableSlot();
        if (slot != -1)
        {
            board.AddOrder(new Order(currentRecipe, Random.Range(1, 5)), bulletinBoard.transform.GetChild(slot), slot);
        }

        // Log the generated recipe for debugging
        Debug.Log("New Recipe Generated: " + string.Join(", ", currentRecipe));
    }

    // Validate the served pasta against the recipe
    public bool ValidateRecipe(List<string> appliedSauces)
    {
        if (board.DoesOrderExist(appliedSauces)) return true;
        return false;
    }

    // Clear the recipe board after serving
    public void DeleteRecipeBoard(List<string> order)
    {
        Order temporder = board.returnOrder(order);
        board.removeOrder(temporder);
        bulletinBoard.transform.GetChild(temporder.boardSlot).GetChild(0).GetComponent<OrderPaper>().destroyOrder();
    }
}
