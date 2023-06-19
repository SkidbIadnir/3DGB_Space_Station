using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Add item if inv is full even is not slot left if maxStack is not reach
Add left amount to a new slot
*/

[Serializable]
public class Inventory : MonoBehaviour
{
    [Header("General")]
    [SerializeField] List<ItemSlot> _container = new List<ItemSlot>();
    public DisplayInventory ui;

    [Header("Settings")]
    public int inventorySize;

    [Header("Market")]
    int _money = 0;

    // public event EventHandler OnItemAdded;
    // public event EventHandler OnItemRemoved;
    public event EventHandler OnInventoryUpdated;

    #region General
    List<ItemSlot> GetItemSlots(Item item) {
        List<ItemSlot> slots = new List<ItemSlot>();
        foreach (ItemSlot slot in _container)
            if (slot.GetItem() == item) slots.Add(slot);
        return slots;
    }
    public List<ItemSlot> GetItems() { return _container; }
    public void ClearInventory() {
        _container.Clear();
        ui.Inventory_OnInventoryUpdated();
    }
    #endregion

    #region AddItem
    public bool AddItem(Item item, int amount, out int remain) {
        remain = 0;
        if (_container.Count >= inventorySize) {
            Debug.Log("Inventory full!");
            return false;
        }

        List<ItemSlot> slots = GetItemSlots(item);

        if (0 == slots.Count) {
            // The inventory does not contain the item
            if (item.GetItemSO().maxStack >= amount) {
                // The amount is smaller than the max stack
                _container.Add(new ItemSlot(item, amount));
                ui.Inventory_OnInventoryUpdated();
                return true;
            } else {
                // The amount added is bigger than the max stack, need more slots
                bool res = TryToAddMoreSlots(item, amount, out int last);
                remain = last;
                ui.Inventory_OnInventoryUpdated();
                return res;
            }
        } else {
            // The inventory already contains the item
            Debug.Log($"a{amount}");
            if (!FillSlotAvailable(slots, amount, out int leftAmount)) {
                // Max stack is reached
                bool res = TryToAddMoreSlots(item, leftAmount, out int last);
                remain = last;
                ui.Inventory_OnInventoryUpdated();
                return res;
            }
            ui.Inventory_OnInventoryUpdated();
            // } else {
            //     // Max stack is not reached
            //     slot.AddAmount(amount);
            //     return true;
            // }
        }
        return false;
    }

    bool FillSlotAvailable(List<ItemSlot> slots, int amount, out int leftAmount) {
        leftAmount = amount;
        Debug.Log("FillSlot");
        foreach (ItemSlot slot in slots) {
            Debug.Log($"l{leftAmount}");
            int remain = slot.GetItem().GetItemSO().maxStack - slot.GetAmount();
            leftAmount -= remain;
            Debug.Log($"l{leftAmount} from r{remain}");
            if (0 > leftAmount) Debug.Log($"{remain + leftAmount}");
            else Debug.Log($"{remain}");
            if (0 > leftAmount) slot.AddAmount(remain + leftAmount);
            else slot.AddAmount(remain);
        }
        Debug.Log($"Still: {leftAmount}");
        return 0 > leftAmount;
    }

    bool TryToAddMoreSlots(Item item, int amount, out int remain) {
        Debug.Log($"amount: {amount} {item.GetItemSO().maxStack} {1 == item.GetItemSO().maxStack}");
        int maxStack = item.GetItemSO().maxStack;
        int slotNeeded = 1;
        if (1 == maxStack) {
            slotNeeded = amount;
            Debug.Log("here");
        }
        else if (maxStack < amount) slotNeeded = amount / maxStack + amount % maxStack;
        Debug.Log($"{amount / maxStack} + {amount % maxStack}");
        remain = amount;

        Debug.Log($"Trying to add {slotNeeded} new slots for {item.GetItemSO().itemName}...");
        Debug.Log($"{_container.Count} >= {inventorySize} => {_container.Count >= inventorySize}");
        while (_container.Count < inventorySize && 0 != slotNeeded) {
            if (1 == slotNeeded) {
                _container.Add(new ItemSlot(item, remain));
            } else {
                _container.Add(new ItemSlot(item, maxStack));
            }
            remain -= maxStack;
            Debug.Log($"remain: {remain}");
            --slotNeeded;
        }
        if (0 != slotNeeded) {
            Debug.Log($"Inventory Full! Was not able to add {remain} {item.GetItemSO().itemName}.");
            return false;
        }
        return true;
    }
    #endregion

    #region RemoveItem
    public bool RemoveItem(Item item, int amount = 0) {
        List<ItemSlot> slots = GetItemSlots(item);
        if (0 == slots.Count) {
            Debug.Log($"No {item.GetItemSO().itemName} in this inventory.");
            return false;
        }
        if (0 == amount) {
            // Remove all slots of that item
            foreach (ItemSlot slot in slots) { _container.Remove(slot); }
        } else {
            // Remove a certain amount of an item
            slots.Reverse();
            int leftAmount = amount;
            Debug.Log(leftAmount);
            foreach (ItemSlot slot in slots) {
                int available = slot.GetAmount();
                if (available >= leftAmount) {
                    slot.RemoveAmount(leftAmount);
                    break;
                } else {
                    slot.RemoveAmount(available);
                    leftAmount -= available;
                }
            }
            foreach (ItemSlot slot in slots) if (0 == slot.GetAmount()) _container.Remove(slot);
        }
        ui.Inventory_OnInventoryUpdated();
        return true;
    }
    #endregion

    #region Market
    // public bool Buy(Item item) {
    //     Item_SO so = item.GetItemSO();
    //     if (so.cost > _money) {
    //         Debug.Log("Not enought money.");
    //         return false;
    //     }
    //     _money -= so.cost;
    //     return true;
    // }
    // public bool Buy(Item_SO so) {
    //     if (so.cost > _money) {
    //         Debug.Log("Not enought money.");
    //         return false;
    //     }
    //     _money -= so.cost;
    //     return true;
    // }
    // public bool Sell() {
    //     throw new NotImplementedException();
    // }
    #endregion

    #region Money
    public int GetMoney() { return _money; }

    public void AddMoney(int value) { _money += value; }
    public void RemoveMoney(int value) { 
        _money -= value;
        if (0 > _money) _money = 0;
    }
    public void SetMoney(int value) {
        _money = value;
        if (0 > _money) _money = 0;
    }
    #endregion
}