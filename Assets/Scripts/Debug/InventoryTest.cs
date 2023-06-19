using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;
    public Item defaultItem;
    public Item nostackItem;
    public Item stack99Item;

    public TMP_InputField AddDefaultField;
    public TMP_InputField RemoveDefaultField;
    public TMP_InputField AddNoStackField;
    public TMP_InputField RemoveNoStackField;
    public TMP_InputField AddStackField;
    public TMP_InputField RemoveStackField;
    
    public void Add(Item item) {
        bool res = inventory.AddItem(item, 1, out int remain);
        Debug.Log($"Add => {res}, remain: {remain}");
    }

    public void RemoveAll(Item item) {
        bool res = inventory.RemoveItem(item);
        Debug.Log($"Remove => {res}");
    }

    public void AddDefault() {
        int amount = 0;
        if (Int32.TryParse(AddDefaultField.text, out int value)) amount = value;
        bool res = inventory.AddItem(defaultItem, amount, out int remain);
        Debug.Log($"Add => {res}, remain: {remain}");
    }
    public void RemoveDefault() {
        int amount = 0;
        if (Int32.TryParse(RemoveDefaultField.text, out int value)) amount = value;
        bool res = inventory.RemoveItem(defaultItem, amount);
        Debug.Log($"Add => {res}");
    }

    public void AddNoStackable() {
        int amount = 0;
        if (Int32.TryParse(AddNoStackField.text, out int value)) amount = value;
        Debug.Log(value);
        bool res = inventory.AddItem(nostackItem, amount, out int remain);
        Debug.Log($"Add => {res}, remain: {remain}");
    }
    public void RemoveNoStackable() {
        int amount = 0;
        if (Int32.TryParse(RemoveNoStackField.text, out int value)) amount = value;
        bool res = inventory.RemoveItem(nostackItem, amount);
        Debug.Log($"Add => {res}");
    }

    public void AddStackable() {
        int amount = 0;
        if (Int32.TryParse(AddStackField.text, out int value)) amount = value;
        bool res = inventory.AddItem(stack99Item, amount, out int remain);
        Debug.Log($"Add => {res}, remain: {remain}");
    }
    public void RemoveStackable() {
        int amount = 0;
        if (Int32.TryParse(RemoveStackField.text, out int value)) amount = value;
        bool res = inventory.RemoveItem(stack99Item, amount);
        Debug.Log($"Add => {res}");
    }
}
