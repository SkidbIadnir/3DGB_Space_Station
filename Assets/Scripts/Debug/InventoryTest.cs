using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;
    public Item defaultItem;

    public void AddDefaultItem(int amount) {
        bool res = inventory.AddItem(defaultItem, amount, out int remain);
        Debug.Log($"{res}: {remain}");
    }
}
