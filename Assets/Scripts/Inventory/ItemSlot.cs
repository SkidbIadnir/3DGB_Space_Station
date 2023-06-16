using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    [SerializeField] Item _item;
    [SerializeField] int _amount;

    public ItemSlot(Item item, int amount = 1) {
        _item = item;
        _amount = amount;
    }

    public Item GetItem() { return _item; }

    public int GetAmount() { return _amount; }
    
    public void AddAmount(int value) { _amount += value; }
    public void RemoveAmount(int value) { 
        _amount -= value;
        if (0 > _amount) _amount = 0;
        //DestroyItself();
    }

    // void DestroyItself() {
    //     // TO DO
    // }
}